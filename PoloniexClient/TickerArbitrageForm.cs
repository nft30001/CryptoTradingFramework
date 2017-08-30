﻿using CryptoMarketClient.Bittrex;
using CryptoMarketClient.Common;
using CryptoMarketClient.Poloniex;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoMarketClient {
    public partial class TickerArbitrageForm : ThreadUpdateForm {
        public TickerArbitrageForm() {
            InitializeComponent();
            UpdateGridFilter(!this.bbAllCurrencies.Checked);
            this.chartSidePanel.Visible = this.bbShowDetail.Checked;
            this.orderBookSidePanel.Visible = this.bcShowOrderBook.Checked;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXScrolling = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
            ((XYDiagram)this.arbitrageHistoryChart.Diagram).AxisY.WholeRange.AlwaysShowZeroLevel = true;
        }
        protected override bool AllowUpdateInactive => true;
        protected override void OnShown(EventArgs e) {
            BuildCurrenciesList();
            base.OnShown(e);
        }
        protected Thread UpdateCurrenciesThread { get; set; }
        protected Thread SyncThread { get; set; }
        protected override void StartUpdateThread() {
            base.StartUpdateThread();
            UpdateCurrenciesThread = CheckStartThread(UpdateCurrenciesThread, UpdateCurrencies);
            SyncThread = CheckStartThread(SyncThread, OnSyncThreadWork);
        }
        protected List<SyncronizationItemInfo> ItemsToSync { get; } = new List<SyncronizationItemInfo>();
        void OnSyncThreadWork() {
            foreach(SyncronizationItemInfo info in ItemsToSync) {
                TelegramBot.Default.SendNotification("start withdrawal: "+ info.Source.MarketCurrency + ": move " + info.Amount.ToString("0.########") + " from " + info.Source.HostName + " to " + info.Destination.HostName);
            }
            while(true) {
                foreach(SyncronizationItemInfo info in ItemsToSync) {
                    if(!info.HasDestinationAddress && !info.ObtainDestinationAddress())
                        continue;
                    if(info.MakeWithdraw()) {
                        ItemsToSync.Remove(info);
                        TelegramBot.Default.SendNotification(info.Source.MarketCurrency + ": moved " + info.Amount.ToString("0.########") + " from " + info.Source.HostName + " to " + info.Destination.HostName);
                        break;
                    }
                    else {
                        Debug.WriteLine("Withdraw failed");
                    }
                }
                Thread.Sleep(30 * 1000);
            }
        }
        protected bool UpdateBalanceNotification { get; set; }
        void UpdateCurrencies() {
            UpdateBalanceNotification = true;
            while(AllowWorkThread) {
                for(int i = 0; i < 3; i++) {
                    if(PoloniexModel.Default.GetBalances())
                        break;
                }
                for(int i = 0; i < 3; i++) {
                    if(PoloniexModel.Default.UpdateCurrencies())
                        break;
                }
                for(int i = 0; i < 3; i++) {
                    if(BittrexModel.Default.GetBalance(ArbitrageList[0].BaseCurrency))
                        break;
                }
                foreach(TickerArbitrageInfo info in ArbitrageList) {
                    for(int i = 0; i < 3; i++) {
                        if(BittrexModel.Default.GetBalance(info.MarketCurrency))
                            break;
                    }
                }
                for(int i = 0; i < 3; i++) {
                    if(BittrexModel.Default.UpdateCurrencies())
                        break;
                }
                if(UpdateBalanceNotification) {
                    UpdateBalanceNotification = false;
                    foreach(PoloniexAccountBalanceInfo info in PoloniexModel.Default.Balances) {
                        if(info.Available > 0)
                            TelegramBot.Default.SendNotification("poloniex balance " + info.Currency + " = <b>" + info.Available.ToString("0.########") + "</b>");
                    }
                    foreach(BittrexAccountBalanceInfo info in BittrexModel.Default.Balances) {
                        if(info.Available > 0)
                            TelegramBot.Default.SendNotification("bittrex  balance " + info.Currency + " = <b>" + info.Available.ToString("0.########") + "</b>");
                    }
                }
                foreach(PoloniexAccountBalanceInfo info in PoloniexModel.Default.Balances) {
                    if(info.DepositChanged > 0.05m) {
                        TelegramBot.Default.SendNotification("poloniex deposit changed: " + info.Currency + " = " + info.Available);
                    }
                }
                foreach(BittrexAccountBalanceInfo info in BittrexModel.Default.Balances) {
                    if(info.DepositChanged > 0.05m) {
                        TelegramBot.Default.SendNotification("bittrex deposit changed: " + info.Currency + " = " + info.Available);
                    }
                }
                Thread.Sleep(5 * 60 * 1000); // sleep 5 min
            }
        }
        void RefreshGridRow(TickerArbitrageInfo info) {
            this.gridView1.RefreshRow(this.gridView1.GetRowHandle(ArbitrageList.IndexOf(info)));
        }
        int concurrentTickersCount = 0;
        Stopwatch timer = new Stopwatch();
        void OnUpdateTickers() {
            timer.Start();
            long lastGUIUpdateTime = 0;
            while(true) {
                for(int i = 0; i < ArbitrageList.Count; i++) {
                    if(ShouldProcessArbitrage)
                        ProcessSelectedArbitrageInfo();
                    if(timer.ElapsedMilliseconds - lastGUIUpdateTime > 2000) {
                        lastGUIUpdateTime = timer.ElapsedMilliseconds;
                        if(IsHandleCreated)
                            BeginInvoke(new Action(RefreshGUI));
                    }
                    if(this.bbMonitorSelected.Checked && !ArbitrageList[i].IsSelected)
                        continue;
                    TickerArbitrageInfo current = ArbitrageList[i];
                    if(current.IsUpdating)
                        continue;
                    if(!current.ObtainingData) {
                        while(concurrentTickersCount > 8) {
                            Thread.Sleep(1);
                            
                        }
                        Interlocked.Increment(ref concurrentTickersCount);
                        UpdateArbitrageInfo(current);
                        //Interlocked.Decrement(ref concurrentTickersCount);
                        continue;
                    }
                    int currentUpdateTimeMS = (int)(timer.ElapsedMilliseconds - current.StartUpdateMs);
                    if(currentUpdateTimeMS > current.NextOverdueMs) {
                        current.UpdateTimeMs = currentUpdateTimeMS;
                        current.IsActual = false;
                        current.NextOverdueMs += 3000;
                        if(IsHandleCreated)
                            BeginInvoke(new Action<TickerArbitrageInfo>(RefreshGridRow), current);
                    }
                    continue;
                }
            }
        }
        async Task UpdateArbitrageInfoTask(TickerArbitrageInfo info) {
            Task task = Task.Factory.StartNew(() => {
                UpdateArbitrageInfo(info);
            });
            await task;
        }
            //int logCounter = 0;
        async void UpdateArbitrageInfo(TickerArbitrageInfo info) {
            //int log = Interlocked.Increment(ref logCounter);
            info.ObtainDataSuccessCount = 0;
            info.ObtainDataCount = 0;
            info.NextOverdueMs = 6000;
            info.StartUpdateMs = timer.ElapsedMilliseconds;
            info.ObtainingData = true;
            Task task = Task.Factory.StartNew(() => {
                for(int i = 0; i < info.Count; i++) {
                        if(info.Tickers[i].UpdateArbitrageOrderBook(TickerArbitrageInfo.Depth))
                            info.ObtainDataSuccessCount++;
                        info.ObtainDataCount++;
                }
                });
            await task;
            Interlocked.Decrement(ref concurrentTickersCount); //todo
            if(info.ObtainDataCount == info.Count) {
                info.IsActual = info.ObtainDataSuccessCount == info.Count;
                info.IsUpdating = true;
                info.ObtainingData = false;
                info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
                OnUpdateTickerInfo(info, true);
            }

            info.LastUpdate = DateTime.Now;
        }

        //async Task UpdateArbitrageInfoOLD(TickerArbitrageInfo info) {
        //    info.ObtainDataSuccessCount = 0;
        //    info.ObtainDataCount = 0;
        //    info.NextOverdueMs = 6000;
        //    info.StartUpdateMs = timer.ElapsedMilliseconds;
        //    info.ObtainingData = true;
        //    for(int i = 0; i < info.Count; i++) {
        //        Task task = Task.Factory.StartNew(() => {
        //            while(info.IsUpdating)
        //                continue;
        //            if(info.Tickers[i].UpdateArbitrageOrderBook(TickerArbitrageInfo.Depth))
        //                info.ObtainDataSuccessCount++;
        //            info.ObtainDataCount++;
        //            if(info.ObtainDataCount == info.Count) {
        //                info.IsActual = info.ObtainDataSuccessCount == info.Count;
        //                info.IsUpdating = true;
        //                info.ObtainingData = false;
        //                info.UpdateTimeMs = (int)(timer.ElapsedMilliseconds - info.StartUpdateMs);
        //                Invoke(new Action<TickerArbitrageInfo, bool>(OnUpdateTickerInfo), info);
        //            }
        //        });
        //        await task;
        //    }
        //    info.LastUpdate = DateTime.Now;
        //}
        protected override void OnThreadUpdate() {
            OnUpdateTickers();
        }
        protected TickerArbitrageInfo SelectedArbitrage { get; set; }
        void ProcessSelectedArbitrageInfo() {
            ShouldProcessArbitrage = false;
            if(SelectedArbitrage == null) {
                LogManager.Default.AddWarning("There is no selected arbitrage info. Quit.");
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            LogManager.Default.Add("Update buy on market balance info.", SelectedArbitrage.LowestAskHost + " - " + SelectedArbitrage.LowestAskTicker.BaseCurrency);
            if(!SelectedArbitrage.LowestAskTicker.UpdateBalance(CurrencyType.BaseCurrency)) {
                LogManager.Default.AddError("Failed update buy on market currency balance. Quit.", SelectedArbitrage.LowestAskTicker.BaseCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Add("Update buy on market balance info.", SelectedArbitrage.HighestBidHost + " - " + SelectedArbitrage.HighestBidTicker.MarketCurrency);
            if(!SelectedArbitrage.HighestBidTicker.UpdateBalance(CurrencyType.MarketCurrency)) {
                LogManager.Default.AddError("Failed update sell on market currency balance. Quit.", SelectedArbitrage.HighestBidTicker.MarketCurrency);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            LogManager.Default.Add("Update arbitrage info values.", SelectedArbitrage.Name);
            if(!UpdateArbitrageInfoTask(SelectedArbitrage).Wait(5000)) {
                LogManager.Default.AddError("Failed arbitrage update info values. Timeout.", SelectedArbitrage.Name);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }
            SelectedArbitrage.IsActual = true;

            SelectedArbitrage.Update();
            SelectedArbitrage.UpateAmountByBalance();
            if(SelectedArbitrage.ExpectedProfitUSD - SelectedArbitrage.MaxProfitUSD > 10)
                LogManager.Default.AddWarning("Arbitrage amount reduced because of balance not enough.", "New Amount = " + SelectedArbitrage.Amount.ToString("0.########") + ", ProfitUSD = " + SelectedArbitrage.MaxProfitUSD);

            if(SelectedArbitrage.AvailableProfitUSD <= 20) {
                LogManager.Default.AddWarning("Arbitrage Profit reduced since last time. Skip trading.", SelectedArbitrage.Name + " expected " + SelectedArbitrage.ExpectedProfitUSD + " but after update" + SelectedArbitrage.MaxProfitUSD);
                Invoke(new MethodInvoker(ShowLog));
                return;
            }

            if(!SelectedArbitrage.Buy()) {
                LogManager.Default.AddError("FATAL ERROR! Could not buy!", SelectedArbitrage.Name);
                return;
            }
            if(!SelectedArbitrage.Sell()) {
                LogManager.Default.AddError("FATAL ERROR! Could not sell!", SelectedArbitrage.Name);
                return;
            }

            string successText = "Arbitrage completed!!! Please check your balances." + SelectedArbitrage.Name + " earned <b>" + SelectedArbitrage.AvailableProfitUSD + "</b>";
            LogManager.Default.AddSuccess(successText);
            TelegramBot.Default.SendNotification(successText);
            UpdateBalanceNotification = true;
            Invoke(new MethodInvoker(ShowLog));
            return;
        }
        void ShowLog() {
            LogManager.Default.Show();
        }
        void RefreshChartDataSource() {
            if(!this.chartSidePanel.Visible)
                return;
            this.arbitrageHistoryChart.RefreshData();
        }
        void RefreshOrderBook() {
            if(!this.orderBookSidePanel.Visible)
                return;
            this.orderBookControl1.RefreshAsks();
            this.orderBookControl1.RefreshBids();
        }
        void RefreshGUI() {
            //this.gridView1.RefreshData();
            RefreshChartDataSource();
            RefreshOrderBook();
        }
        void OnUpdateTickerInfo(TickerArbitrageInfo info, bool useInvokeForUI = false) {
            decimal prevProfits = info.MaxProfitUSD;
            info.IsUpdating = true;
            info.Update();
            info.SaveExpectedProfitUSD();
            info.IsUpdating = false;
            bool checkMaxProfits = true;
            if(info.AvailableProfitUSD > 20) {
                SelectedArbitrage = info;
                ShouldProcessArbitrage = true;
                checkMaxProfits = false;
            }
            var action = new Action(() => {
                RefreshGridRow(info);
                if(checkMaxProfits && info.MaxProfitUSD - prevProfits > 20)
                    ShowNotification(info, prevProfits);
            });
            if(useInvokeForUI)
                BeginInvoke(action);
            else
                action();

        }
        void ShowDesktopNotification(TickerArbitrageInfo info, decimal prev) {
            if(MdiParent.WindowState != FormWindowState.Minimized)
                return;
            decimal delta = info.MaxProfitUSD - prev;
            decimal percent = delta / prev * 100;

            string changed = string.Empty;
            TrendNotification trend = TrendNotification.New;
            if(prev > 0) {
                changed = "Arbitrage changed: <b>" + percent.ToString("+0.###;-0.###;0.###%%") + "</b>";
                trend = delta > 0 ? TrendNotification.TrendUp : TrendNotification.TrendDown;
            }
            else
                changed = "New Arbitrage possibilities. Up to <b>" + info.MaxProfitUSD.ToString("USD 0.###") + "</b>";
            GetReadyNotificationForm().ShowInfo(this, trend, info.ShortName, changed, 10000);
        }
        void ShowNotification(TickerArbitrageInfo info, decimal prev) {
            SendTelegramNotification(info, prev);
            ShowDesktopNotification(info, prev);
        }
        void SendTelegramNotification(TickerArbitrageInfo info, decimal prev) {
            if(prev <= 0 && info.MaxProfit <= 0)
                return;
            string text = string.Empty;
            string eventText = string.Empty;
            if(info.AvailableAmount > 0) {
                if(prev <= 0)
                    eventText = prev <= 0 ? "<b>new</b> " : "<b>changed</b> ";
                text = eventText + info.ShortName;
                text += " spread: " + info.Spread.ToString("0.########");
                text += " amount: " + info.Amount.ToString("0.########");
                text += " max profit: " + info.MaxProfitUSD.ToString("0.###");
                text += " avail profit: " + info.AvailableProfitUSD.ToString("0.###");
                TelegramBot.Default.SendNotification(text);
            }
        }
        protected List<NotificationForm> NotificationForms { get; } = new List<NotificationForm>();
        NotificationForm GetReadyNotificationForm() {
            for(int i = 0; i < NotificationForms.Count; i++) {
                if(NotificationForms[i].IsDisposed)
                    NotificationForms[i] = new NotificationForm();
                if(!NotificationForms[i].Visible)
                    return NotificationForms[i];
            }
            NotificationForms.Add(new NotificationForm());
            return NotificationForms[NotificationForms.Count - 1];
        }
        public List<TickerArbitrageInfo> ArbitrageList { get; private set; }
        void BuildCurrenciesList() {
            ArbitrageList = TickerArbitrageHelper.GetArbitrageInfoList();
            ArbitrageList = ArbitrageList.Where((i) => i.BaseCurrency == "BTC").ToList();
            tickerArbitrageInfoBindingSource.DataSource = ArbitrageList;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            TickerArbitrageHelper.Update(ArbitrageList);
            this.gridControl1.RefreshDataSource();
        }

        private void bbShowDetail_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.chartSidePanel.Visible = this.bbShowDetail.Checked;
            if(this.chartSidePanel.Visible) {
                UpdateChartData();
            }
        }

        void UpdateChartData() {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            if(gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            this.arbitrageHistoryChart.DataSource = info.History;
            this.arbitrageHistoryChart.Legend.Title.Visible = true;
            this.arbitrageHistoryChart.Legend.Title.Text = info.Name;
        }

        private void bbAllCurrencies_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            UpdateGridFilter(!((BarCheckItem)e.Item).Checked);
        }
        void UpdateGridFilter(bool showAll) {
            if(showAll)
                this.gridView1.ActiveFilterString = null;
            else
                this.gridView1.ActiveFilterCriteria = new BinaryOperator("MaxProfit", 0, BinaryOperatorType.Greater);
        }

        private void gridView1_Click(object sender, EventArgs e) {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
            bbTryArbitrage.Tag = info;
            bbTryArbitrage.Caption = "Try Arbitrage on " + info.Name;
            if(this.chartSidePanel.Visible)
                UpdateChartData();
            if(this.orderBookSidePanel.Visible)
                UpdateOrderBook();
        }

        private void TickerArbitrageForm_Load(object sender, EventArgs e) {

        }

        private void bcShowOrderBook_CheckedChanged(object sender, ItemClickEventArgs e) {
            this.orderBookSidePanel.Visible = this.bcShowOrderBook.Checked;
            UpdateOrderBook();
        }
        void UpdateOrderBook() {
            if(!this.orderBookSidePanel.Visible)
                return;
            if(this.gridView1.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            this.orderBookControl1.ArbitrageInfo = (TickerArbitrageInfo)this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
        }

        protected bool ShouldProcessArbitrage { get; set; }
        private void bbTryArbitrage_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)bbTryArbitrage.Tag;
            ShouldProcessArbitrage = SelectedArbitrage != null;
        }

        private void bbOpenWeb_ItemClick(object sender, ItemClickEventArgs e) {
            TickerArbitrageInfo info = (TickerArbitrageInfo)bbTryArbitrage.Tag;
            if(info == null) {
                XtraMessageBox.Show("Arbitrage not selected!");
                return;
            }
            for(int i = 0; i < info.Count; i++) {
                System.Diagnostics.Process.Start(info.Tickers[i].WebPageAddress);
            }
        }

        private void bbSelectPositive_ItemClick(object sender, ItemClickEventArgs e) {
            foreach(TickerArbitrageInfo info in ArbitrageList) {
                info.IsSelected = info.MaxProfitUSD > 0;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void bbBuy_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)this.bbTryArbitrage.Tag;
            if(SelectedArbitrage == null)
                return;

            ITicker lowest = SelectedArbitrage.LowestAskTicker;

            if(!lowest.UpdateBalance(CurrencyType.BaseCurrency)) {
                LogManager.Default.AddError("Cant update balance.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }

            decimal percent = Convert.ToDecimal(this.beBuyLowestAsk.EditValue) / 100;
            decimal buyAmount = lowest.BaseCurrencyBalance * percent;
            LogManager.Default.Add("Lowest Ask Base Currency Amount = " + buyAmount.ToString("0.########"));
            decimal amount = buyAmount / SelectedArbitrage.LowestAsk;

            if(!SelectedArbitrage.LowestAskTicker.Buy(SelectedArbitrage.LowestAsk, amount))
                LogManager.Default.AddError("Cant buy currency.", "At " + lowest.HostName + "-" + lowest.BaseCurrency + "(" + amount.ToString("0.########") + ")" + " for " + lowest.MarketCurrency);

            SelectedArbitrage = null;
            LogManager.Default.Show();
            return;
        }

        private void bbSell_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)this.bbTryArbitrage.Tag;
            if(SelectedArbitrage == null)
                return;

            ITicker highest = SelectedArbitrage.HighestBidTicker;
            if(!highest.UpdateBalance(CurrencyType.MarketCurrency)) {
                LogManager.Default.AddError("Cant update balance.", highest.HostName + "-" + highest.MarketCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }

            decimal percent = Convert.ToDecimal(this.beHighestBidSell.EditValue) / 100;
            decimal amount = highest.MarketCurrencyBalance * percent;
            LogManager.Default.Add("Highest Bid Market Currency Amount = " + amount.ToString("0.########"));

            if(!SelectedArbitrage.HighestBidTicker.Sell(SelectedArbitrage.HighestBid, amount))
                LogManager.Default.AddError("Cant sell currency.", "At " + highest.HostName + "-" + highest.MarketCurrency + "(" + amount.ToString("0.########") + ")" + " for " + highest.BaseCurrency);

            SelectedArbitrage = null;
        }

        protected virtual bool SyncToHighestBid(TickerArbitrageInfo info, bool forceUpdateBalance, bool allowLog) {
            if(info == null)
                return false;

            ITicker lowest = info.LowestAskTicker;
            ITicker highest = info.HighestBidTicker;

            if(forceUpdateBalance || lowest.MarketCurrencyBalance == 0) {
                if(!lowest.UpdateBalance(CurrencyType.MarketCurrency)) {
                    if(allowLog) LogManager.Default.AddError("Cant update balance.", lowest.HostName + "-" + lowest.MarketCurrency);
                    if(allowLog) LogManager.Default.Show();
                    return false;
                }
                if(!highest.UpdateBalance(CurrencyType.MarketCurrency)) {
                    if(allowLog) LogManager.Default.AddError("Cant update balance.", highest.HostName + "-" + highest.MarketCurrency);
                    if(allowLog) LogManager.Default.Show();
                    return false;
                }
            }
            string highAddress = highest.GetDepositAddress(CurrencyType.MarketCurrency);
            if(string.IsNullOrEmpty(highAddress)) {
                if(allowLog) LogManager.Default.AddError("Cant get deposit address.", highest.HostName + "-" + highest.MarketCurrency);
                if(allowLog) LogManager.Default.Show();
                return false;
            }

            if(allowLog) LogManager.Default.Add("Highest Bid Currency Deposit: " + highAddress);

            decimal amount = lowest.MarketCurrencyBalance;
            if(allowLog)LogManager.Default.Add("Lowest Ask Currency Amount = " + amount.ToString("0.########"));

            if(lowest.Withdraw(CurrencyType.MarketCurrency, highAddress, amount)) {
                string text = "Withdraw " + lowest.MarketCurrency + " " + lowest.HostName + " -> " + highest.HostName + " succeded.";
                if(allowLog) LogManager.Default.AddSuccess(text);
                TelegramBot.Default.SendNotification(text);
                return true;
            }
            else {
                if(allowLog) LogManager.Default.AddError("Withdraw " + lowest.MarketCurrency + " " + lowest.HostName + " -> " + highest.HostName + " failed.");
                return false;
            }
        }

        private void bbSendToHighestBid_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)this.bbTryArbitrage.Tag;
            if(SelectedArbitrage == null)
                return;
            SyncToHighestBid(SelectedArbitrage, true, true);
            LogManager.Default.Show();
            SelectedArbitrage = null;
        }

        private void bbSyncWithLowestAsk_ItemClick(object sender, ItemClickEventArgs e) {
            SelectedArbitrage = (TickerArbitrageInfo)this.bbTryArbitrage.Tag;
            if(SelectedArbitrage == null)
                return;
            ITicker lowest = SelectedArbitrage.LowestAskTicker;
            ITicker highest = SelectedArbitrage.HighestBidTicker;

            if(!lowest.UpdateBalance(CurrencyType.BaseCurrency)) {
                LogManager.Default.AddError("Cant update balance.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }
            if(!highest.UpdateBalance(CurrencyType.BaseCurrency)) {
                LogManager.Default.AddError("Cant update balance.", highest.HostName + "-" + highest.BaseCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }

            string lowAddress = lowest.GetDepositAddress(CurrencyType.BaseCurrency);
            if(string.IsNullOrEmpty(lowAddress)) {
                LogManager.Default.AddError("Cant get deposit address.", lowest.HostName + "-" + lowest.BaseCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }

            string highAddress = highest.GetDepositAddress(CurrencyType.BaseCurrency);
            if(string.IsNullOrEmpty(highAddress)) {
                LogManager.Default.AddError("Cant get deposit address.", highest.HostName + "-" + highest.BaseCurrency);
                SelectedArbitrage = null;
                LogManager.Default.Show();
                return;
            }

            LogManager.Default.Add("Lowest Ask Base Currency Deposit: " + lowAddress);
            LogManager.Default.Add("Highest Bid Base Currency Deposit: " + highAddress);

            decimal amount = highest.BaseCurrencyBalance;
            LogManager.Default.Add("Highest Bid Base Currency Amount = " + amount.ToString("0.########"));

            highest.Withdraw(CurrencyType.BaseCurrency, lowAddress, amount);

            LogManager.Default.Show();
            SelectedArbitrage = null;
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e) {
            gridView1.PostEditor();
        }

        private void bbShowHistory_ItemClick(object sender, ItemClickEventArgs e) {
            ArbitrageHistoryForm form = new ArbitrageHistoryForm();
            form.MdiParent = MdiParent;
            form.Show();
        }

        private void btShowCombinedBidAsk_ItemClick(object sender, ItemClickEventArgs e) {
            TickerArbitrageInfo info = (TickerArbitrageInfo)this.bbTryArbitrage.Tag;
            if(info == null)
                return;
            CombinedBidAskForm form = new CombinedBidAskForm();
            form.MdiParent = MdiParent;
            form.AddTicker(info.LowestAskTicker);
            form.AddTicker(info.HighestBidTicker);
            form.Text = info.Name;
            form.Show();
        }
    }
}
