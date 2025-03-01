﻿using Crypto.Core.Exchanges.Base;
using Crypto.Core.Helpers;
using Crypto.Core.Strategies.Custom;
using Crypto.Core;
using Crypto.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Crypto.Core.Strategies {
    public class SimulationStrategyDataProvider : IStrategyDataProvider {
        public SimulationStrategyDataProvider() {
            StartTime = DateTime.MaxValue;
            EndTime = DateTime.MinValue;
        }

        bool IStrategyDataProvider.IsFinished { get { return FinishedCore; } }
        protected bool FinishedCore { get; set; }
        bool IStrategyDataProvider.Connect(StrategyInputInfo info) {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MaxValue;
            if(info.Strategy is ISimulationDataProvider) {
                ((ISimulationDataProvider)info.Strategy).Connect();

                StrategySimulationData data = GetSimulationData(info.Strategy);
                if(data != null) {
                    data.Connected = true;
                    StartTime = MaxTime(StartTime, data.GetStartTime());
                    EndTime = MinTime(EndTime, data.GetEndTime());
                }
                return true;
            }
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];

                if(ti.Ticker.CandleStickData != null)
                    ti.Ticker.CandleStickData.Clear();
                ti.Ticker.ClearTradeHistory();

                ti.Ticker.Exchange.EnterSimulationMode();
                ti.Ticker.Exchange.StartListenTickerStream(ti.Ticker);
                //ti.Ticker.Exchange.StartListenOrderBook(ti.Ticker);
                //ti.Ticker.Exchange.StartListenTradeHistory(ti.Ticker);
                //ti.Ticker.Exchange.StartListenTickerStream(ti.Ticker);

                StrategySimulationData data = GetSimulationData(ti.Ticker);
                if(data == null)
                    return false;
                data.Connected = true;
                StartTime = MaxTime(StartTime, data.GetStartTime());
                EndTime = MinTime(EndTime, data.GetEndTime());
            }
            LastTime = StartTime;
            return true;
        }

        public DateTime MaxTime(DateTime tm1, DateTime tm2) {
            return tm1 > tm2 ? tm1 : tm2;
        }

        public DateTime MinTime(DateTime tm1, DateTime tm2) {
            return tm1 < tm2 ? tm1 : tm2;
        }

        [XmlIgnore]
        public DateTime StartTime { get; private set; }
        [XmlIgnore]
        public DateTime EndTime { get; private set; }
        public double SimulationProgress { get; set; } = 0.0;
        public double DownloadProgress { get; set; } = 0.0;
        public event TickerDownloadProgressEventHandler DownloadProgressChanged;
        public string DownloadText { get; set; } = "Downloading...";

        public ResizeableArray<TradeInfoItem> DownloadTradeHistory(TickerInputInfo info, DateTime time) {
            info.CheckUpdateTime();

            CachedTradeHistory savedData = new CachedTradeHistory() { Exchange = info.Exchange, TickerName = info.TickerName, StartDate = info.StartDate, EndDate = info.EndDate, BaseCurrency = info.Ticker.BaseCurrency, MarketCurrency = info.Ticker.MarketCurrency };
            CachedTradeHistory cachedData = CachedTradeHistory.FromFile(CachedTradeHistory.Directory + "\\" + ((ISupportSerialization)savedData).FileName);
            
            if(cachedData != null && cachedData.Items.Count > 0) {
                if(info.StartDate != DateTime.MinValue && info.StartDate == cachedData.Items[0].Time.Date && info.EndDate.AddDays(-1) <= cachedData.Items.Last().Time.Date) {
                    LogManager.Default.Add(LogType.Log, this, "" + info.Exchange + ":" + info.TickerName, "load cached trade history", cachedData.StartDate + "-" + cachedData.EndDate + ": " + cachedData.Items.Count + " items");
                    return cachedData.Items;
                }
            }

            DateTime start = info.StartDate.Date;
            int intervalInSeconds = info.KlineIntervalMin * 60;
            DateTime origin = start;
            DateTime end = info.EndDate.Date.AddHours(12);
            if(end > DateTime.Now)
                end = DateTime.Now.AddHours(-1);
            ResizeableArray<TradeInfoItem> res = new ResizeableArray<TradeInfoItem>();
            DownloadProgress = 0.0;
            DownloadText = "Donwloading Trade History Data for " + info.Exchange + ":" + info.TickerName;

            while(start < end) {
                DateTime localEnd = start.AddDays(1);
                if(localEnd > end)
                    localEnd = end;
                ResizeableArray<TradeInfoItem> data = info.Ticker.Exchange.GetTrades(info.Ticker, start, localEnd);
                if(data == null || data.Count == 0) {
                    LogManager.Default.Add(LogType.Error, this, info.TickerName, "cannot download trade history", start + "-" + localEnd);
                    start = start.AddDays(1);
                    continue;
                }
                localEnd = data.Last().Time;
                DownloadProgress = (data.Last().Time - origin).TotalMinutes / (DateTime.UtcNow - origin).TotalMinutes * 100;
                var args = RaiseDownloadProgressChanged();
                if(args != null && args.Cancel)
                    return null;
                //Thread.Sleep(300);
                LogManager.Default.Add(LogType.Success, this, info.TickerName, "trade history downloaded", start + "-" + localEnd);
                if(res.Last() != null && res.Last().Time == data[0].Time) {
                    DownloadProgress = 100;
                    break;
                }
                start = data.Last().Time.AddSeconds(+1);
                res.AddRange(data);
            }

            RaiseDownloadProgressChanged();

            for(int i = 0; i < res.Count - 1; i++) {
                if(res[i].Time > res[i + 1].Time) {
                    LogManager.Default.Add(LogType.Success, this, info.TickerName, "trade history timing error", start + "-" + end);
                    throw new Exception("TradeHistory Timing Error!");
                }
            }
            
            cachedData = savedData;
            cachedData.Items = res;
            if(savedData.Items.Count > 0) {
                savedData.StartDate = savedData.Items[0].Time;
                savedData.EndDate = savedData.Items.Last().Time;
            }
            cachedData.Save();

            return res;
        }

        [XmlIgnore]
        public DateTime LastTime { get; private set; } = DateTime.Now;
        DateTime IStrategyDataProvider.CurrentTime { get { return LastTime; } }
        void IStrategyDataProvider.OnTick() {
            DateTime nextTime = GetNextTime(LastTime);
            if(nextTime == DateTime.MaxValue) {
                FinishedCore = true;
                return;
            }
            UpdateTickersOrderBook();
            SendEventsWithTime(nextTime);
            LastTime = nextTime;
            SimulationProgress = (LastTime - StartTime).TotalMilliseconds / (EndTime - StartTime).TotalMilliseconds;
        }

        protected virtual void UpdateTickersOrderBook() {
            foreach(StrategySimulationData data in SimulationData.Values) {
                if(data.SimulationData != null)
                    continue;
                if(data.UseTickerSimulationDataFile)
                    continue;
                if(data.TickerInfo.UseKline) {
                    if(!data.HasCandlesticksLeft)
                        continue;
                    data.UpdateCandleStickItem();
                }
                else if(data.TickerInfo.UseTradeHistory) {
                    if(!data.HasTradeLeft)
                        continue;
                    data.UpdateCurrentBidByTradeItem();
                }
                data.Ticker.OrderBook.Offset(data.CurrentBid);
            }
        }

        protected virtual void SendEventsWithTime(DateTime time) {
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.SimulationData != null) {
                    ((ISimulationDataProvider)s.Strategy).OnNewItem(s.NextSimulationItem());
                }
                else if(s.UseTickerSimulationDataFile) {
                    s.Ticker.ApplyCapturedEvent(time);
                }
                else if(s.TickerInfo.UseKline)
                    s.CheckSendCandleStickItem();
                else if(s.TickerInfo.UseTradeHistory)
                    s.CheckSendTradeItem(time);
            }
        }

        protected virtual DateTime GetNextTime(DateTime prevTime) {
            DateTime minTime = DateTime.MaxValue;
            foreach(StrategySimulationData s in SimulationData.Values) {
                if(!s.Connected)
                    continue;
                if(s.SimulationData != null) {
                    minTime = s.GetNextTime(prevTime);
                }
                if(s.UseTickerSimulationDataFile) {
                    if(s.Ticker.CaptureDataHistory.CurrentItem == null)
                        continue;
                    DateTime time = s.Ticker.CaptureDataHistory.CurrentItem.Time;
                    if(minTime > time)
                        minTime = time;
                }
                else if(s.TickerInfo != null){
                    if(s.TickerInfo.UseKline || s.TickerInfo.UseTradeHistory) {
                        DateTime time = s.GetNextTime(prevTime); 
                        if(minTime > time)
                            minTime = time;
                    }
                }
            }
            return minTime;
        }

        private StrategySimulationData GetSimulationData(object ti) {
            StrategySimulationData data;
            if(SimulationData.TryGetValue(ti, out data)) return data;
            return null;
        }

        bool IStrategyDataProvider.Disconnect(StrategyInputInfo info) {
            if(info.Strategy is ISimulationDataProvider) {
                ((ISimulationDataProvider)info.Strategy).Disconnect();
                StrategySimulationData data = GetSimulationData(info.Strategy);
                if(data != null) data.Connected = false;
                return true;
            }
            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                ti.Ticker.Exchange.ExitSimulationMode();
                StrategySimulationData data = GetSimulationData(ti.Ticker);
                if(data != null) data.Connected = false;
            }
            return true;
        }

        protected Dictionary<ExchangeType, Exchange> Exchanges { get; } = new Dictionary<ExchangeType, Exchange>();

        Exchange IStrategyDataProvider.GetExchange(ExchangeType exchange) {
            if(Exchanges.ContainsKey(exchange))
                return Exchanges[exchange];

            Exchange e = Exchange.CreateExchange(exchange);
            if(e.Tickers.Count == 0 && !e.GetTickersInfo())
                return null;

            Exchanges.Add(exchange, e);
            return e;
        }

        void IStrategyDataProvider.Reset() {
            StartTime = DateTime.MaxValue;
            EndTime = DateTime.MinValue;
            FinishedCore = false;
            SimulationProgress = 0.0;
        }

        protected bool LoadDataForStrategy(StrategyBase s) {
            if(SimulationData.ContainsKey(s))
                return true;
            StrategySimulationData data = new StrategySimulationData() { Strategy = s };
            data.SimulationData = ((ISimulationDataProvider)s).LoadData();
            if(data.SimulationData == null)
                return false;
            SimulationData.Add(s, data);
            return true;
        }

        bool IStrategyDataProvider.InitializeDataFor(StrategyBase s) {
            LogManager.Default.Add(LogType.Log, this, s.Name, "initialize data for simulation", "");
            if(s is ISimulationDataProvider)
                return LoadDataForStrategy(s);
            return LoadDataForTickers(s);
        }

        protected bool LoadDataForTickers(StrategyBase s) {
            StrategyInputInfo info = s.CreateInputInfo();

            for(int i = 0; i < info.Tickers.Count; i++) {
                TickerInputInfo ti = info.Tickers[i];
                Exchange e = ((IStrategyDataProvider)this).GetExchange(ti.Exchange);
                if(e == null)
                    return false;
                ti.Ticker = e.GetTicker(ti.TickerName);
                if(ti.Ticker == null)
                    return false;
                StrategySimulationData data = null;
                SimulationData.TryGetValue(ti.Ticker, out data);
                if(data == null)
                    data = new StrategySimulationData() { Ticker = ti.Ticker, Exchange = e, Strategy = s, TickerInfo = ti };
                if(!string.IsNullOrEmpty(ti.TickerSimulationDataFile)) {
                    data.UseTickerSimulationDataFile = true;
                    if(!ti.Ticker.CaptureDataHistory.Load(ti.TickerSimulationDataFile))
                        return false;
                    data.CaptureDataHistory = ti.Ticker.CaptureDataHistory;
                }
                else {
                    if(ti.UseTradeHistory) {
                        if(data.Trades == null)
                            data.Trades = DownloadTrades(ti);
                    }
                    if(ti.UseKline) {
                        if(data.CandleStickData == null)
                            data.CandleStickData = DownloadCandleStickData(ti);
                        data.CopyCandleSticksFromLoaded();
                        if(data.CandleStickData.Count == 0)
                            return false;
                    }
                    data.OrderBook = DownloadOrderBook(ti);
                    if(data.OrderBook == null)
                        return false;
                }
                if(!SimulationData.ContainsKey(ti.Ticker))
                    SimulationData.Add(ti.Ticker, data);
            }
            return true;
        }

        protected OrderBook DownloadOrderBook(TickerInputInfo ti) {
            int orderBookDepth = ti.OrderBookDepth == 0 ? OrderBook.Depth : ti.OrderBookDepth;
            if(!ti.Ticker.UpdateOrderBook(orderBookDepth))
                return null;
            if(ti.Ticker.UsdTicker != null)
                ti.Ticker.UsdTicker.UpdateOrderBook(orderBookDepth);
            OrderBook ob = new OrderBook(null);
            ob.Assign(ti.Ticker.OrderBook);
            return ob;
        }

        public virtual ResizeableArray<TradeInfoItem> DownloadTrades(TickerInputInfo info) {
            ResizeableArray<TradeInfoItem> trades = DownloadTradeHistory(info, info.StartDate);
            return trades;
        }

        public virtual ResizeableArray<CandleStickData> DownloadCandleStickData(TickerInputInfo info) {
            CachedCandleStickData savedData = new CachedCandleStickData() { Exchange = info.Exchange, TickerName = info.TickerName, IntervalMin = info.KlineIntervalMin, StartDate = info.StartDate, EndDate = info.EndDate, BaseCurrency = info.Ticker.BaseCurrency, MarketCurrency = info.Ticker.MarketCurrency };
            CachedCandleStickData cachedData = CachedCandleStickData.FromFile(CachedCandleStickData.Directory + "\\" + ((ISupportSerialization)savedData).FileName);
            info.CheckUpdateTime();
            if(cachedData != null) {
                if(info.StartDate != DateTime.MinValue && info.StartDate == cachedData.Items[0].Time.Date && info.EndDate == cachedData.Items.Last().Time.Date)
                    return cachedData.Items;
            }

            DateTime start = info.StartDate;
            int intervalInSeconds = info.KlineIntervalMin * 60;
            ResizeableArray<CandleStickData> res = new ResizeableArray<CandleStickData>();
            int deltaCount = 500;
            long minCount = (long)(SettingsStore.Default.SimulationSettings.KLineHistoryIntervalDays) * 24 * 60;
            long candleStickCount = minCount / info.KlineIntervalMin;
            long pageCount = candleStickCount / deltaCount;
            int pageIndex = 0;
            DownloadProgress = 0.0;
            DownloadText = "Donwloading CandleStick Data for " + info.Exchange + ":" + info.TickerName;

            if(!info.Ticker.Exchange.SupportCandleSticksRange) {
                ResizeableArray<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null)
                    return null;
                DownloadProgress = 100;
                var args = RaiseDownloadProgressChanged();
                if(args.Cancel)
                    return null;

                cachedData = savedData;
                cachedData.Items = data;
                cachedData.Save();
                return data;
            }

            while(start.Date < info.EndDate.Date) { 
                ResizeableArray<CandleStickData> data = info.Ticker.GetCandleStickData(info.KlineIntervalMin, start, intervalInSeconds * deltaCount);
                if(data == null || data.Count == 0) {
                    start = start.AddSeconds(intervalInSeconds * deltaCount);
                    continue;
                }
                res.AddRange(data);
                pageIndex++;
                DownloadProgress = pageIndex * 100 / pageCount;
                RaiseDownloadProgressChanged();
                Thread.Sleep(300);
                start = start.AddSeconds(intervalInSeconds * deltaCount);
            }
            cachedData = savedData;
            cachedData.Items = res;
            cachedData.Save();
            return res;   
        }

        private TickerDownloadProgressEventArgs RaiseDownloadProgressChanged() {
            if(DownloadProgressChanged != null) {
                TickerDownloadProgressEventArgs res = new TickerDownloadProgressEventArgs() { DownloadText = DownloadText, DownloadProgress = DownloadProgress };
                DownloadProgressChanged(this, res);
                return res;
            }
            return null;
        }

        protected Dictionary<object, StrategySimulationData> SimulationData { get; } = new Dictionary<object, StrategySimulationData>();

        AccountInfo IStrategyDataProvider.GetAccount(Guid accountId) {
            return null;
        }
    }

    public class StrategySimulationData {
        public bool Connected { get; set; }
        public Exchange Exchange { get; set; }
        public Ticker Ticker { get; set; }
        public StrategyBase Strategy { get; set; }
        public ResizeableArray<CandleStickData> CandleStickData { get; set; }
        public ResizeableArray<TradeInfoItem> Trades { get; set; }
        public CandleStickData NextCandleStick { get; set; }
        public CandleStickData NextFinalCandleStick { get; set; }
        public int CurrentPriceIteration { get; set; }
        public int MaxPriceIterationCount { get { return 20; } }
        public double CurrentBid { get; set; }
        public OrderBook OrderBook { get; set; }
        public ExchangeInputInfo ExchangeInfo { get; set; }
        public TickerInputInfo TickerInfo { get; set; }
        public bool UseTickerSimulationDataFile { get; set; }
        public TickerCaptureData CaptureDataHistory { get; internal set; }
        public int CurrentCandleStickDataItemIndex { get; set; } = 0;
        public int CurrentTradeItemIndex { get; set; } = 0;

        public bool CopyCandleSticksFromLoaded() {
            CurrentCandleStickDataItemIndex = 0;
            CandleStickIntervalSeconds = (int)(CandleStickData[1].Time - CandleStickData[0].Time).TotalSeconds;
            NextFinalCandleStick = CandleStickData[CurrentCandleStickDataItemIndex];
            NextCandleStick = CreateStartCandleStick(NextFinalCandleStick);
            return true;
        }

        public DateTime MaxTime(DateTime tm1, DateTime tm2) {
            return tm1 > tm2 ? tm1 : tm2;
        }

        public DateTime MinTime(DateTime tm1, DateTime tm2) {
            return tm1 < tm2 ? tm1 : tm2;
        }

        public DateTime GetStartTime() {
            DateTime start = DateTime.MinValue;
            if(SimulationData != null)
                return SimulationData[0].Time;
            if(CaptureDataHistory != null)
                start = MaxTime(start, CaptureDataHistory.Items.First().Time);
            if(CandleStickData != null)
                start = MaxTime(start, CandleStickData.First().Time);
            if(Trades != null && Trades.Count > 0)
                start = MaxTime(start, Trades[0].Time);
            return start;
        }

        public DateTime GetEndTime() {
            DateTime end = DateTime.MaxValue;
            if(SimulationData != null)
                return SimulationData[SimulationData.Count - 1].Time;
            if(CaptureDataHistory != null)
                end = MinTime(end, CaptureDataHistory.Items.Last().Time);
            if(CandleStickData != null)
                end = MinTime(end, CandleStickData.Last().Time);
            if(Trades != null && Trades.Count > 0)
                end = MinTime(end, Trades.Last().Time);
            return end;
        }

        internal void UpdateCurrentBid() {
            double x = CurrentPriceIteration / (double)MaxPriceIterationCount;
            if(x < 0.33) {
                CurrentBid = NextFinalCandleStick.Open + x * (NextFinalCandleStick.Low - NextFinalCandleStick.Open) / 0.33;
                NextCandleStick.Low = Math.Min(NextCandleStick.Low, CurrentBid);
                NextCandleStick.High = Math.Max(NextCandleStick.High, CurrentBid);
                NextCandleStick.Close = CurrentBid;
            }
            else if(x < 0.66) {
                CurrentBid = NextFinalCandleStick.Low + (x - 0.33) * (NextFinalCandleStick.High - NextFinalCandleStick.Low) / 0.33;
                NextCandleStick.Low = NextFinalCandleStick.Low;
                NextCandleStick.High = Math.Max(NextCandleStick.High, CurrentBid);
                NextCandleStick.Close = CurrentBid;
            }
            else if(CurrentPriceIteration < MaxPriceIterationCount - 1) {
                NextCandleStick.High = NextFinalCandleStick.High;
                NextCandleStick.Close = CurrentBid;
                CurrentBid = NextFinalCandleStick.High + (x - 0.66) * (NextFinalCandleStick.Close - NextFinalCandleStick.High) / 0.33;
            }
            else {
                NextCandleStick.Close = NextFinalCandleStick.Close;
                CurrentBid = NextFinalCandleStick.Close;
            }
        }
        public int CandleStickIntervalSeconds { get; set; }
        public bool HasCandlesticksLeft { get { return CurrentCandleStickDataItemIndex < CandleStickData.Count; } }
        public bool HasTradeLeft { get { return CurrentTradeItemIndex < Trades.Count; } }

        public ResizeableArray<IInputDataWithTime> SimulationData { get; internal set; }
        protected int SimulationDataItemIndex { get; set; } = 0;
        public IInputDataWithTime NextSimulationItem() {
            if(SimulationDataItemIndex >= SimulationData.Count)
                return null;
            IInputDataWithTime res = SimulationData[SimulationDataItemIndex];
            SimulationDataItemIndex++;
            return res;
        }
        public DateTime GetNextTime(DateTime prevTime) {
            if(SimulationData != null) {
                if(SimulationDataItemIndex >= SimulationData.Count)
                    return DateTime.MaxValue;
                return SimulationData[SimulationDataItemIndex].Time;
            }

            if(TickerInfo.UseKline) {
                if(!HasCandlesticksLeft)
                    return DateTime.MaxValue;

                double seconds = CandleStickIntervalSeconds * ((double)CurrentPriceIteration) / MaxPriceIterationCount;
                return NextCandleStick.Time.AddSeconds(seconds);
            }
            else if(TickerInfo.UseTradeHistory) {
                if(!HasTradeLeft)
                    return DateTime.MaxValue;
                return GetTradeAfterOrEqual(prevTime);
            }
            return DateTime.MaxValue;
        }

        private DateTime GetTradeAfterOrEqual(DateTime prevTime) {
            while(CurrentTradeItemIndex < Trades.Count) {
                if(Trades[CurrentTradeItemIndex].Time > prevTime)
                    return Trades[CurrentTradeItemIndex].Time;
                CurrentTradeItemIndex++;
            }
            return DateTime.MaxValue;
        }

        protected CandleStickData CreateStartCandleStick(CandleStickData data) {
            CandleStickData res = new Crypto.Core.CandleStickData();
            res.Assign(data);
            res.Open = res.Close = res.High = res.Low = data.Open;
            return res;
        }

        public void UpdateCandleStickItem() {
            if(CurrentCandleStickDataItemIndex == CandleStickData.Count)
                return;
            if(CurrentPriceIteration == MaxPriceIterationCount) {
                NextFinalCandleStick = CandleStickData[CurrentCandleStickDataItemIndex];
                NextCandleStick = CreateStartCandleStick(NextFinalCandleStick);
                CurrentCandleStickDataItemIndex++;
                CurrentBid = NextCandleStick.Open;
                CurrentPriceIteration = 0;
                return;
            }
            
            UpdateCurrentBid();
            CurrentPriceIteration++;
        }

        public void UpdateCurrentBidByTradeItem() {
            if(CurrentTradeItemIndex == Trades.Count)
                return;
            CurrentBid = Trades[CurrentTradeItemIndex].Rate;
        }

        public void CheckSendCandleStickItem() {
            if(CurrentCandleStickDataItemIndex == CandleStickData.Count)
                return;
            Ticker.UpdateCandleStickData(NextCandleStick);
        }

        public void CheckSendTradeItem(DateTime time) {
            if(CurrentTradeItemIndex == Trades.Count)
                return;
            if(Trades[CurrentTradeItemIndex].Time == time) {
                Ticker.UpdateSimulationTradeData(Trades[CurrentTradeItemIndex]);
                //CurrentTradeItemIndex++;
            }
        }
    }

    [Serializable]
    public class CachedCandleStickData : ISupportSerialization {
        public static string Directory { get { return "SimulationData\\CandleStickData"; } }
        public static CachedCandleStickData FromFile(string fileName) {
            return (CachedCandleStickData)SerializationHelper.FromFile(fileName, typeof(CachedCandleStickData));
        }

        void ISupportSerialization.OnEndDeserialize() {
            
        }

        public bool Save() {
            return SerializationHelper.Save(this, typeof(CachedCandleStickData), Directory);
        }

        string ISupportSerialization.FileName { get {
                return TickerDownloadDataInfo.GetCandlestickCachedDataFileName(Exchange, BaseCurrency, MarketCurrency, IntervalMin, StartDate, EndDate);
            } set { } }

        public ResizeableArray<CandleStickData> Items { get; set; } = new ResizeableArray<CandleStickData>();
        public ExchangeType Exchange { get; set; }
        public string TickerName { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public int IntervalMin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TickerDownloadDataInfo {
        public bool Selected { get; set; }
        public TickerDataType Type { get; set; }
        public ExchangeType Exchange { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public int KLineIntervalMin { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileName { get; set; }

        public static string DateTime2String(DateTime value) {
            string res = value.ToString("d", CultureInfo.InvariantCulture);
            return res.Replace(CultureInfo.InvariantCulture.DateTimeFormat.DateSeparator, "#");
        }

        public static DateTime DateTimeFromString(string str) {
            str = str.Replace("#", CultureInfo.InvariantCulture.DateTimeFormat.DateSeparator);
            return DateTime.Parse(str, CultureInfo.InvariantCulture);
        }

        public static string GetCandlestickCachedDataFileName(ExchangeType e, string baseCurrency, string marketCurrency, int intervalMin, DateTime start, DateTime end) {
            return e.ToString() + "_" + baseCurrency + "_" + marketCurrency + "_" + intervalMin + "_" +
                DateTime2String(start) + "_" +
                DateTime2String(end) + "_" +
                "CandlestickData.xml";
        }

        public static string GetTradeHistoryCachedDataFileName(ExchangeType e, string baseCurrency, string marketCurrency, DateTime start, DateTime end) {
            return e.ToString() + "_" + baseCurrency + "_" + marketCurrency + "_" +
                DateTime2String(start) + "_" +
                DateTime2String(end) + "_" +
                "TradeHistory.xml";
        }

        public void GenerateFileName() {
            if(Type == TickerDataType.Candlesticks)
                FileName = GetCandlestickCachedDataFileName(Exchange, BaseCurrency, MarketCurrency, KLineIntervalMin, StartDate, EndDate);
            else 
                FileName = GetTradeHistoryCachedDataFileName(Exchange, BaseCurrency, MarketCurrency, StartDate, EndDate);
        }
    }
    public enum TickerDataType {
        Candlesticks,
        TradeHistory
    }

    [Serializable]
    public class CachedTradeHistory : ISupportSerialization {
        public static string Directory { get { return "SimulationData\\TradeHistory"; } }
        public static CachedTradeHistory FromFile(string fileName) {
            return (CachedTradeHistory)SerializationHelper.FromFile(fileName, typeof(CachedTradeHistory));
        }

        void ISupportSerialization.OnEndDeserialize() {

        }

        public bool Save() {
            return SerializationHelper.Save(this, typeof(CachedTradeHistory), Directory);
        }

        string ISupportSerialization.FileName {
            get {
                return TickerDownloadDataInfo.GetTradeHistoryCachedDataFileName(Exchange, BaseCurrency, MarketCurrency, StartDate, EndDate); }
            set { } }

        public ResizeableArray<TradeInfoItem> Items { get; set; } = new ResizeableArray<TradeInfoItem>();
        public ExchangeType Exchange { get; set; }
        public string TickerName { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class SimulationSettings {
        public DateTime StartTime { get { return DateTime.UtcNow.Date.AddDays(-KLineHistoryIntervalDays); } }
        public int KLineHistoryIntervalDays { get; set; } = 30;

        public void Assign(SimulationSettings st) {
            KLineHistoryIntervalDays = st.KLineHistoryIntervalDays;
        }
        public SimulationSettings Clone() {
            SimulationSettings res = new SimulationSettings();
            res.Assign(this);
            return res;
        }
    }

    public class TickerDownloadProgressEventArgs {
        public string DownloadText { get; set; }
        public double DownloadProgress { get; set; }
        public bool Cancel { get; set; } = false;
    }
    public delegate void TickerDownloadProgressEventHandler(object sender, TickerDownloadProgressEventArgs e);
}
