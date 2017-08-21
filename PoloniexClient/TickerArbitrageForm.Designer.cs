﻿using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CryptoMarketClient {
    partial class TickerArbitrageForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule3 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue3 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            DevExpress.XtraGrid.GridFormatRule gridFormatRule4 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue4 = new DevExpress.XtraEditors.FormatConditionRuleValue();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TickerArbitrageForm));
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView3 = new DevExpress.XtraCharts.StepAreaSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StepAreaSeriesView stepAreaSeriesView4 = new DevExpress.XtraCharts.StepAreaSeriesView();
            this.colEarning = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActual = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEarningUSD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tickerArbitrageInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIsSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colBaseCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarketCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTickers = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidTicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAskHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidHost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAsk = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpread = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLowestAksFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHighestBidFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalFee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbShowDetail = new DevExpress.XtraBars.BarCheckItem();
            this.bbAllCurrencies = new DevExpress.XtraBars.BarCheckItem();
            this.bcShowOrderBook = new DevExpress.XtraBars.BarCheckItem();
            this.bbTryArbitrage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bbMonitorSelected = new DevExpress.XtraBars.BarCheckItem();
            this.bbOpenWeb = new DevExpress.XtraBars.BarButtonItem();
            this.bbSelectPositive = new DevExpress.XtraBars.BarButtonItem();
            this.bbBuy = new DevExpress.XtraBars.BarButtonItem();
            this.bbSell = new DevExpress.XtraBars.BarButtonItem();
            this.bbSendToHighestBid = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.rpPoloniex = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.arbitrageHistoryChart = new DevExpress.XtraCharts.ChartControl();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.chartSidePanel = new DevExpress.XtraEditors.SidePanel();
            this.orderBookSidePanel = new DevExpress.XtraEditors.SidePanel();
            this.orderBookControl1 = new CryptoMarketClient.OrderBookControl();
            this.beBuyLowestAsk = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.beHighestBidSell = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bbSyncWithLowestAsk = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView4)).BeginInit();
            this.sidePanel1.SuspendLayout();
            this.chartSidePanel.SuspendLayout();
            this.orderBookSidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // colEarning
            // 
            this.colEarning.DisplayFormat.FormatString = "0.########";
            this.colEarning.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEarning.FieldName = "Earning";
            this.colEarning.Name = "colEarning";
            this.colEarning.OptionsColumn.AllowEdit = false;
            this.colEarning.OptionsColumn.ReadOnly = true;
            this.colEarning.Visible = true;
            this.colEarning.VisibleIndex = 14;
            // 
            // colIsActual
            // 
            this.colIsActual.Caption = "IsActual";
            this.colIsActual.FieldName = "IsActual";
            this.colIsActual.Name = "colIsActual";
            this.colIsActual.OptionsColumn.AllowEdit = false;
            // 
            // colUpdateTime
            // 
            this.colUpdateTime.Caption = "UpdateTime Ms";
            this.colUpdateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colUpdateTime.FieldName = "UpdateTimeMs";
            this.colUpdateTime.Name = "colUpdateTime";
            this.colUpdateTime.OptionsColumn.AllowEdit = false;
            this.colUpdateTime.Visible = true;
            this.colUpdateTime.VisibleIndex = 4;
            // 
            // colEarningUSD
            // 
            this.colEarningUSD.Caption = "In USD";
            this.colEarningUSD.DisplayFormat.FormatString = "0.########";
            this.colEarningUSD.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colEarningUSD.FieldName = "EarningUSD";
            this.colEarningUSD.Name = "colEarningUSD";
            this.colEarningUSD.OptionsColumn.AllowEdit = false;
            this.colEarningUSD.OptionsColumn.ReadOnly = true;
            this.colEarningUSD.Visible = true;
            this.colEarningUSD.VisibleIndex = 15;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.tickerArbitrageInfoBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(688, 377);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tickerArbitrageInfoBindingSource
            // 
            this.tickerArbitrageInfoBindingSource.DataSource = typeof(CryptoMarketClient.TickerArbitrageInfo);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIsSelected,
            this.colBaseCurrency,
            this.colMarketCurrency,
            this.colLastUpdate,
            this.colUpdateTime,
            this.colIsActual,
            this.colTickers,
            this.colCount,
            this.colLowestAskTicker,
            this.colHighestBidTicker,
            this.colLowestAskHost,
            this.colHighestBidHost,
            this.colLowestAsk,
            this.colHighestBid,
            this.colSpread,
            this.colAmount,
            this.colBuyTotal,
            this.colTotal,
            this.colLowestAksFee,
            this.colHighestBidFee,
            this.colTotalFee,
            this.colEarning,
            this.colEarningUSD});
            gridFormatRule3.Column = this.colEarning;
            gridFormatRule3.ColumnApplyTo = this.colEarning;
            gridFormatRule3.Name = "ArbitrageSpreadRule";
            formatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            formatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue3.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue3.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Greater;
            formatConditionRuleValue3.Value1 = 0D;
            gridFormatRule3.Rule = formatConditionRuleValue3;
            gridFormatRule4.Column = this.colIsActual;
            gridFormatRule4.ColumnApplyTo = this.colUpdateTime;
            gridFormatRule4.Name = "FormatNotActual";
            formatConditionRuleValue4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            formatConditionRuleValue4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            formatConditionRuleValue4.Appearance.Options.UseBackColor = true;
            formatConditionRuleValue4.Appearance.Options.UseForeColor = true;
            formatConditionRuleValue4.Condition = DevExpress.XtraEditors.FormatCondition.Equal;
            formatConditionRuleValue4.Value1 = false;
            gridFormatRule4.Rule = formatConditionRuleValue4;
            this.gridView1.FormatRules.Add(gridFormatRule3);
            this.gridView1.FormatRules.Add(gridFormatRule4);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colSpread, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // colIsSelected
            // 
            this.colIsSelected.Caption = "Selected";
            this.colIsSelected.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsSelected.FieldName = "IsSelected";
            this.colIsSelected.Name = "colIsSelected";
            this.colIsSelected.Visible = true;
            this.colIsSelected.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colBaseCurrency
            // 
            this.colBaseCurrency.Caption = "Base";
            this.colBaseCurrency.FieldName = "BaseCurrency";
            this.colBaseCurrency.Name = "colBaseCurrency";
            this.colBaseCurrency.OptionsColumn.AllowEdit = false;
            this.colBaseCurrency.Visible = true;
            this.colBaseCurrency.VisibleIndex = 1;
            // 
            // colMarketCurrency
            // 
            this.colMarketCurrency.Caption = "Market";
            this.colMarketCurrency.FieldName = "MarketCurrency";
            this.colMarketCurrency.Name = "colMarketCurrency";
            this.colMarketCurrency.OptionsColumn.AllowEdit = false;
            this.colMarketCurrency.Visible = true;
            this.colMarketCurrency.VisibleIndex = 2;
            // 
            // colLastUpdate
            // 
            this.colLastUpdate.DisplayFormat.FormatString = "HH:mm:ss.fff";
            this.colLastUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastUpdate.FieldName = "LastUpdate";
            this.colLastUpdate.Name = "colLastUpdate";
            this.colLastUpdate.OptionsColumn.AllowEdit = false;
            this.colLastUpdate.Visible = true;
            this.colLastUpdate.VisibleIndex = 3;
            // 
            // colTickers
            // 
            this.colTickers.FieldName = "Tickers";
            this.colTickers.Name = "colTickers";
            this.colTickers.OptionsColumn.AllowEdit = false;
            this.colTickers.OptionsColumn.ReadOnly = true;
            // 
            // colCount
            // 
            this.colCount.FieldName = "Count";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.OptionsColumn.ReadOnly = true;
            // 
            // colLowestAskTicker
            // 
            this.colLowestAskTicker.FieldName = "LowestAskTicker";
            this.colLowestAskTicker.Name = "colLowestAskTicker";
            this.colLowestAskTicker.OptionsColumn.AllowEdit = false;
            this.colLowestAskTicker.OptionsColumn.ReadOnly = true;
            // 
            // colHighestBidTicker
            // 
            this.colHighestBidTicker.FieldName = "HighestBidTicker";
            this.colHighestBidTicker.Name = "colHighestBidTicker";
            this.colHighestBidTicker.OptionsColumn.AllowEdit = false;
            this.colHighestBidTicker.OptionsColumn.ReadOnly = true;
            // 
            // colLowestAskHost
            // 
            this.colLowestAskHost.Caption = "Buy On";
            this.colLowestAskHost.FieldName = "LowestAskHost";
            this.colLowestAskHost.Name = "colLowestAskHost";
            this.colLowestAskHost.OptionsColumn.AllowEdit = false;
            this.colLowestAskHost.OptionsColumn.ReadOnly = true;
            this.colLowestAskHost.Visible = true;
            this.colLowestAskHost.VisibleIndex = 5;
            // 
            // colHighestBidHost
            // 
            this.colHighestBidHost.Caption = "Sell On";
            this.colHighestBidHost.FieldName = "HighestBidHost";
            this.colHighestBidHost.Name = "colHighestBidHost";
            this.colHighestBidHost.OptionsColumn.AllowEdit = false;
            this.colHighestBidHost.OptionsColumn.ReadOnly = true;
            this.colHighestBidHost.Visible = true;
            this.colHighestBidHost.VisibleIndex = 6;
            // 
            // colLowestAsk
            // 
            this.colLowestAsk.DisplayFormat.FormatString = "0.########";
            this.colLowestAsk.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLowestAsk.FieldName = "LowestAsk";
            this.colLowestAsk.Name = "colLowestAsk";
            this.colLowestAsk.OptionsColumn.AllowEdit = false;
            this.colLowestAsk.OptionsColumn.ReadOnly = true;
            this.colLowestAsk.Visible = true;
            this.colLowestAsk.VisibleIndex = 7;
            // 
            // colHighestBid
            // 
            this.colHighestBid.DisplayFormat.FormatString = "0.########";
            this.colHighestBid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHighestBid.FieldName = "HighestBid";
            this.colHighestBid.Name = "colHighestBid";
            this.colHighestBid.OptionsColumn.AllowEdit = false;
            this.colHighestBid.OptionsColumn.ReadOnly = true;
            this.colHighestBid.Visible = true;
            this.colHighestBid.VisibleIndex = 8;
            // 
            // colSpread
            // 
            this.colSpread.DisplayFormat.FormatString = "0.#########";
            this.colSpread.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSpread.FieldName = "Spread";
            this.colSpread.Name = "colSpread";
            this.colSpread.OptionsColumn.AllowEdit = false;
            this.colSpread.Visible = true;
            this.colSpread.VisibleIndex = 9;
            // 
            // colAmount
            // 
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 10;
            // 
            // colBuyTotal
            // 
            this.colBuyTotal.DisplayFormat.FormatString = "0.########";
            this.colBuyTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBuyTotal.FieldName = "BuyTotal";
            this.colBuyTotal.Name = "colBuyTotal";
            this.colBuyTotal.OptionsColumn.AllowEdit = false;
            this.colBuyTotal.Visible = true;
            this.colBuyTotal.VisibleIndex = 11;
            // 
            // colTotal
            // 
            this.colTotal.DisplayFormat.FormatString = "0.########";
            this.colTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 12;
            // 
            // colLowestAksFee
            // 
            this.colLowestAksFee.FieldName = "LowestAksFee";
            this.colLowestAksFee.Name = "colLowestAksFee";
            this.colLowestAksFee.OptionsColumn.AllowEdit = false;
            this.colLowestAksFee.OptionsColumn.ReadOnly = true;
            // 
            // colHighestBidFee
            // 
            this.colHighestBidFee.FieldName = "HighestBidFee";
            this.colHighestBidFee.Name = "colHighestBidFee";
            this.colHighestBidFee.OptionsColumn.AllowEdit = false;
            this.colHighestBidFee.OptionsColumn.ReadOnly = true;
            // 
            // colTotalFee
            // 
            this.colTotalFee.DisplayFormat.FormatString = "0.########";
            this.colTotalFee.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalFee.FieldName = "TotalFee";
            this.colTotalFee.Name = "colTotalFee";
            this.colTotalFee.OptionsColumn.AllowEdit = false;
            this.colTotalFee.OptionsColumn.ReadOnly = true;
            this.colTotalFee.Visible = true;
            this.colTotalFee.VisibleIndex = 13;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowGlyphSkinning = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.bbShowDetail,
            this.bbAllCurrencies,
            this.bcShowOrderBook,
            this.bbTryArbitrage,
            this.barButtonItem1,
            this.bbMonitorSelected,
            this.bbOpenWeb,
            this.bbSelectPositive,
            this.bbBuy,
            this.bbSell,
            this.bbSendToHighestBid,
            this.beBuyLowestAsk,
            this.beHighestBidSell,
            this.bbSyncWithLowestAsk});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 17;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpPoloniex,
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemSpinEdit2,
            this.repositoryItemSpinEdit3});
            this.ribbonControl1.Size = new System.Drawing.Size(1179, 141);
            // 
            // bbShowDetail
            // 
            this.bbShowDetail.Caption = "Show Chart";
            this.bbShowDetail.Id = 1;
            this.bbShowDetail.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbShowDetail.ImageOptions.Image")));
            this.bbShowDetail.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbShowDetail.ImageOptions.LargeImage")));
            this.bbShowDetail.Name = "bbShowDetail";
            this.bbShowDetail.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbShowDetail_CheckedChanged);
            // 
            // bbAllCurrencies
            // 
            this.bbAllCurrencies.Caption = "Show Positive Earnings";
            this.bbAllCurrencies.Id = 3;
            this.bbAllCurrencies.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbAllCurrencies.ImageOptions.Image")));
            this.bbAllCurrencies.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbAllCurrencies.ImageOptions.LargeImage")));
            this.bbAllCurrencies.Name = "bbAllCurrencies";
            this.bbAllCurrencies.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bbAllCurrencies_CheckedChanged);
            // 
            // bcShowOrderBook
            // 
            this.bcShowOrderBook.Caption = "Combined OrderBook";
            this.bcShowOrderBook.Id = 4;
            this.bcShowOrderBook.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bcShowOrderBook.ImageOptions.Image")));
            this.bcShowOrderBook.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bcShowOrderBook.ImageOptions.LargeImage")));
            this.bcShowOrderBook.Name = "bcShowOrderBook";
            this.bcShowOrderBook.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bcShowOrderBook_CheckedChanged);
            // 
            // bbTryArbitrage
            // 
            this.bbTryArbitrage.Caption = "Try Arbitrage!";
            this.bbTryArbitrage.Id = 5;
            this.bbTryArbitrage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbTryArbitrage.ImageOptions.Image")));
            this.bbTryArbitrage.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbTryArbitrage.ImageOptions.LargeImage")));
            this.bbTryArbitrage.Name = "bbTryArbitrage";
            this.bbTryArbitrage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbTryArbitrage_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Monitor only Seelcted Arbitrage";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bbMonitorSelected
            // 
            this.bbMonitorSelected.Caption = "Monitor Only Selected Arbitrages";
            this.bbMonitorSelected.Id = 7;
            this.bbMonitorSelected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.Image")));
            this.bbMonitorSelected.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbMonitorSelected.ImageOptions.LargeImage")));
            this.bbMonitorSelected.Name = "bbMonitorSelected";
            // 
            // bbOpenWeb
            // 
            this.bbOpenWeb.Caption = "Open Markets in Web";
            this.bbOpenWeb.Id = 8;
            this.bbOpenWeb.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbOpenWeb.ImageOptions.Image")));
            this.bbOpenWeb.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbOpenWeb.ImageOptions.LargeImage")));
            this.bbOpenWeb.Name = "bbOpenWeb";
            this.bbOpenWeb.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbOpenWeb_ItemClick);
            // 
            // bbSelectPositive
            // 
            this.bbSelectPositive.Caption = "Select Positive Arbitrages";
            this.bbSelectPositive.Id = 9;
            this.bbSelectPositive.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("bbSelectPositive.ImageOptions.Image")));
            this.bbSelectPositive.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("bbSelectPositive.ImageOptions.LargeImage")));
            this.bbSelectPositive.Name = "bbSelectPositive";
            this.bbSelectPositive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSelectPositive_ItemClick);
            // 
            // bbBuy
            // 
            this.bbBuy.Caption = "Buy LowestAsk";
            this.bbBuy.Id = 10;
            this.bbBuy.Name = "bbBuy";
            this.bbBuy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbBuy_ItemClick);
            // 
            // bbSell
            // 
            this.bbSell.Caption = "Sell Highest Bid";
            this.bbSell.Id = 11;
            this.bbSell.Name = "bbSell";
            this.bbSell.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSell_ItemClick);
            // 
            // bbSendToHighestBid
            // 
            this.bbSendToHighestBid.Caption = "Sync With Highest Bid Market";
            this.bbSendToHighestBid.Id = 12;
            this.bbSendToHighestBid.Name = "bbSendToHighestBid";
            this.bbSendToHighestBid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSendToHighestBid_ItemClick);
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // rpPoloniex
            // 
            this.rpPoloniex.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpPoloniex.Name = "rpPoloniex";
            this.rpPoloniex.Text = "Connect";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.bbShowDetail);
            this.ribbonPageGroup1.ItemLinks.Add(this.bcShowOrderBook);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbAllCurrencies, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbSelectPositive);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbMonitorSelected);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbOpenWeb);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbTryArbitrage);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Arbitrage";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.bbBuy);
            this.ribbonPageGroup2.ItemLinks.Add(this.beBuyLowestAsk);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSendToHighestBid);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSell, true);
            this.ribbonPageGroup2.ItemLinks.Add(this.beHighestBidSell);
            this.ribbonPageGroup2.ItemLinks.Add(this.bbSyncWithLowestAsk);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Manual Arbitrage";
            // 
            // arbitrageHistoryChart
            // 
            this.arbitrageHistoryChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.arbitrageHistoryChart.DataBindings = null;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.WholeRange.AutoSideMargins = false;
            xyDiagram2.AxisX.WholeRange.SideMarginsValue = 0D;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.arbitrageHistoryChart.Diagram = xyDiagram2;
            this.arbitrageHistoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arbitrageHistoryChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
            this.arbitrageHistoryChart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.arbitrageHistoryChart.Legend.Name = "Default Legend";
            this.arbitrageHistoryChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.arbitrageHistoryChart.Location = new System.Drawing.Point(1, 0);
            this.arbitrageHistoryChart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.arbitrageHistoryChart.Name = "arbitrageHistoryChart";
            series3.ArgumentDataMember = "Time";
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series3.LegendText = "Earnings in USD";
            series3.Name = "Earnings in USD";
            series3.ValueDataMembersSerializable = "ValueUSD";
            series3.View = stepAreaSeriesView3;
            series4.ArgumentDataMember = "Time";
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series4.LegendText = "Earnings in base currency";
            series4.Name = "Earnings in base currency";
            series4.ValueDataMembersSerializable = "Value";
            series4.View = stepAreaSeriesView4;
            this.arbitrageHistoryChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3,
        series4};
            this.arbitrageHistoryChart.Size = new System.Drawing.Size(264, 377);
            this.arbitrageHistoryChart.TabIndex = 5;
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.gridControl1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel1.Location = new System.Drawing.Point(0, 141);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(2);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(688, 377);
            this.sidePanel1.TabIndex = 6;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // chartSidePanel
            // 
            this.chartSidePanel.Controls.Add(this.arbitrageHistoryChart);
            this.chartSidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.chartSidePanel.Location = new System.Drawing.Point(914, 141);
            this.chartSidePanel.Margin = new System.Windows.Forms.Padding(2);
            this.chartSidePanel.Name = "chartSidePanel";
            this.chartSidePanel.Size = new System.Drawing.Size(265, 377);
            this.chartSidePanel.TabIndex = 7;
            this.chartSidePanel.Text = "sidePanel2";
            // 
            // orderBookSidePanel
            // 
            this.orderBookSidePanel.Controls.Add(this.orderBookControl1);
            this.orderBookSidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.orderBookSidePanel.Location = new System.Drawing.Point(688, 141);
            this.orderBookSidePanel.Margin = new System.Windows.Forms.Padding(2);
            this.orderBookSidePanel.Name = "orderBookSidePanel";
            this.orderBookSidePanel.Size = new System.Drawing.Size(226, 377);
            this.orderBookSidePanel.TabIndex = 9;
            this.orderBookSidePanel.Text = "sidePanel2";
            // 
            // orderBookControl1
            // 
            this.orderBookControl1.ArbitrageInfo = null;
            this.orderBookControl1.Asks = null;
            this.orderBookControl1.Bids = null;
            this.orderBookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderBookControl1.Location = new System.Drawing.Point(1, 0);
            this.orderBookControl1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.orderBookControl1.Name = "orderBookControl1";
            this.orderBookControl1.OrderBookCaption = "";
            this.orderBookControl1.Size = new System.Drawing.Size(225, 377);
            this.orderBookControl1.TabIndex = 0;
            // 
            // beBuyLowestAsk
            // 
            this.beBuyLowestAsk.Caption = "Use Base Currency (0-100%)";
            this.beBuyLowestAsk.Edit = this.repositoryItemSpinEdit2;
            this.beBuyLowestAsk.EditValue = 50D;
            this.beBuyLowestAsk.EditWidth = 75;
            this.beBuyLowestAsk.Id = 14;
            this.beBuyLowestAsk.Name = "beBuyLowestAsk";
            // 
            // repositoryItemSpinEdit2
            // 
            this.repositoryItemSpinEdit2.AutoHeight = false;
            this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Manual Arbitrage";
            // 
            // beHighestBidSell
            // 
            this.beHighestBidSell.Caption = "Use Market Currency (0-100%)";
            this.beHighestBidSell.Edit = this.repositoryItemSpinEdit3;
            this.beHighestBidSell.EditValue = 100D;
            this.beHighestBidSell.EditWidth = 75;
            this.beHighestBidSell.Id = 15;
            this.beHighestBidSell.Name = "beHighestBidSell";
            // 
            // repositoryItemSpinEdit3
            // 
            this.repositoryItemSpinEdit3.AutoHeight = false;
            this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit3.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
            // 
            // bbSyncWithLowestAsk
            // 
            this.bbSyncWithLowestAsk.Caption = "Send To Lowest Ask";
            this.bbSyncWithLowestAsk.Id = 16;
            this.bbSyncWithLowestAsk.Name = "bbSyncWithLowestAsk";
            this.bbSyncWithLowestAsk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbSyncWithLowestAsk_ItemClick);
            // 
            // TickerArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 518);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.orderBookSidePanel);
            this.Controls.Add(this.chartSidePanel);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "TickerArbitrageForm";
            this.Text = "Classic Arbitrage";
            this.Load += new System.EventHandler(this.TickerArbitrageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tickerArbitrageInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stepAreaSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arbitrageHistoryChart)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            this.chartSidePanel.ResumeLayout(false);
            this.orderBookSidePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GridControl gridControl1;
        private GridView gridView1;
        private System.Windows.Forms.BindingSource tickerArbitrageInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colMarketCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colTickers;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidTicker;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAskHost;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidHost;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAsk;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBid;
        private DevExpress.XtraGrid.Columns.GridColumn colSpread;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLowestAksFee;
        private DevExpress.XtraGrid.Columns.GridColumn colHighestBidFee;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalFee;
        private DevExpress.XtraGrid.Columns.GridColumn colEarning;
        private DevExpress.XtraGrid.Columns.GridColumn colEarningUSD;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdate;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpPoloniex;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarCheckItem bbShowDetail;
        private DevExpress.XtraCharts.ChartControl arbitrageHistoryChart;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.SidePanel chartSidePanel;
        private DevExpress.XtraBars.BarCheckItem bbAllCurrencies;
        private DevExpress.XtraBars.BarCheckItem bcShowOrderBook;
        private DevExpress.XtraEditors.SidePanel orderBookSidePanel;
        private OrderBookControl orderBookControl1;
        private DevExpress.XtraBars.BarButtonItem bbTryArbitrage;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActual;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarCheckItem bbMonitorSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem bbOpenWeb;
        private DevExpress.XtraBars.BarButtonItem bbSelectPositive;
        private DevExpress.XtraBars.BarButtonItem bbBuy;
        private DevExpress.XtraBars.BarButtonItem bbSell;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem bbSendToHighestBid;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.BarEditItem beBuyLowestAsk;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarEditItem beHighestBidSell;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit3;
        private DevExpress.XtraBars.BarButtonItem bbSyncWithLowestAsk;
    }
}