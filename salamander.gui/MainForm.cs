using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace com.defrobo.salamander.gui
{
    public partial class MainForm : Form
    {
        private InfoService infoService;
        private Dictionary<Currency, Balance> balances;
        private OrderBook orderBook;
        private IAlerter alerter;
        private Predictor predictor;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void Startup()
        {
            var backtest = true;

            infoService = new InfoService();
            var balancesResult = infoService.GetBalances();

            if (backtest)
            {
                alerter = new BackTestReplayer(".\\..\\..\\..\\salamander.backtestrecorder\\bin\\debug\\recording0.bts");
            }
            else
            {
                alerter = new Alerter();
            }

            alerter.ExecutionCreated += ExecutionAlerter_Created;
            alerter.OrderBookSnapshot += OrderBookUpdater_Snapshot;
            alerter.OrderBookUpdated += OrderBookUpdater_Update;
            alerter.TickerUpdated += Ticker_Updated;
            orderBook = new OrderBook(alerter);
            predictor = new Predictor(alerter);

            alerter.Start();


            balances = await balancesResult;
            RefreshBalances(balances);
        }

        private void OrderBookUpdater_Update(object sender, OrderBookUpdateEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                lstOrderBookBids.Items.Clear();
                lstOrderBookAsks.Items.Clear();

                foreach (var order in orderBook.Bids.Take(orderBook.Bids.Count >= 10 ? 10 : orderBook.Bids.Count))
                {
                    lstOrderBookBids.Items.Add(String.Format("{0} {1}", order.Price, order.Size));
                }

                foreach (var order in orderBook.Asks.Take(orderBook.Asks.Count >= 10 ? 10 : orderBook.Asks.Count))
                {
                    lstOrderBookAsks.Items.Add(String.Format("{0} {1}", order.Price, order.Size));
                }

            }));
        }

        private void OrderBookUpdater_Snapshot(object sender, OrderBookSnapshotEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                lstOrderBookBids.Items.Clear();
                lstOrderBookAsks.Items.Clear();

                foreach (var order in orderBook.Bids.Take(10))
                {
                    lstOrderBookBids.Items.Add(String.Format("{0} {1}", order.Price, order.Size));
                }

                foreach (var order in orderBook.Asks.Take(10))
                {
                    lstOrderBookAsks.Items.Add(String.Format("{0} {1}", order.Price, order.Size));
                }

            }));
        }

        private void Ticker_Updated(object sender, MarketTickEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                lblBestBid.Text = e.Tick.BestBid.ToString();
                lblBestBidSize.Text = e.Tick.BestBidSize.ToString();
                lblLastTradedPrice.Text = e.Tick.LastTradedPrice.ToString();
                lblBestAsk.Text = e.Tick.BestAsk.ToString();
                lblBestAskSize.Text = e.Tick.BestAskSize.ToString();

                lblDirection.Text = predictor.PriceDirection.ToString();
            }));
        }

        private void ExecutionAlerter_Created(object sender, ExecutionEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                foreach (var execution in e.Executions)
                {
                    lstExecutions.Items.Add(String.Format("{0} {1} {2}", execution.Side, execution.Price, execution.Size));
                }
            }));
        }

        private void UpdateExecutions(ExecutionEventArgs e)
        {
            foreach (var execution in e.Executions)
            {
                lstExecutions.Items.Add(String.Format("{0} {1} {2}", execution.Side, execution.Price, execution.Size));
            }
        }

        private void RefreshBalances(Dictionary<Currency, Balance> balances)
        {
            lblBalanceJPYAmount.Text = balances[Currency.JPY].Amount.ToString();
            lblBalanceBTCAmount.Text = balances[Currency.BTC].Amount.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Startup();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            infoService.Close();
            alerter.Stop();
        }
    }
}
