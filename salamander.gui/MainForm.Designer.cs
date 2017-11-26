namespace com.defrobo.salamander.gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblBalanceJPY = new System.Windows.Forms.Label();
            this.lblBalanceJPYAmount = new System.Windows.Forms.Label();
            this.lblBalanceBTC = new System.Windows.Forms.Label();
            this.lblBalanceBTCAmount = new System.Windows.Forms.Label();
            this.lstExecutions = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBestBid = new System.Windows.Forms.Label();
            this.lblBestBidSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLastTradedPrice = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBestAsk = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBestAskSize = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lstOrderBookBids = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lstOrderBookAsks = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblDirection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBalanceJPY
            // 
            this.lblBalanceJPY.AutoSize = true;
            this.lblBalanceJPY.Location = new System.Drawing.Point(12, 34);
            this.lblBalanceJPY.Name = "lblBalanceJPY";
            this.lblBalanceJPY.Size = new System.Drawing.Size(26, 13);
            this.lblBalanceJPY.TabIndex = 0;
            this.lblBalanceJPY.Text = "JPY";
            // 
            // lblBalanceJPYAmount
            // 
            this.lblBalanceJPYAmount.AutoSize = true;
            this.lblBalanceJPYAmount.Location = new System.Drawing.Point(44, 34);
            this.lblBalanceJPYAmount.Name = "lblBalanceJPYAmount";
            this.lblBalanceJPYAmount.Size = new System.Drawing.Size(13, 13);
            this.lblBalanceJPYAmount.TabIndex = 1;
            this.lblBalanceJPYAmount.Text = "0";
            // 
            // lblBalanceBTC
            // 
            this.lblBalanceBTC.AutoSize = true;
            this.lblBalanceBTC.Location = new System.Drawing.Point(12, 57);
            this.lblBalanceBTC.Name = "lblBalanceBTC";
            this.lblBalanceBTC.Size = new System.Drawing.Size(28, 13);
            this.lblBalanceBTC.TabIndex = 2;
            this.lblBalanceBTC.Text = "BTC";
            // 
            // lblBalanceBTCAmount
            // 
            this.lblBalanceBTCAmount.AutoSize = true;
            this.lblBalanceBTCAmount.Location = new System.Drawing.Point(44, 57);
            this.lblBalanceBTCAmount.Name = "lblBalanceBTCAmount";
            this.lblBalanceBTCAmount.Size = new System.Drawing.Size(13, 13);
            this.lblBalanceBTCAmount.TabIndex = 3;
            this.lblBalanceBTCAmount.Text = "0";
            // 
            // lstExecutions
            // 
            this.lstExecutions.FormattingEnabled = true;
            this.lstExecutions.Location = new System.Drawing.Point(417, 22);
            this.lstExecutions.Name = "lstExecutions";
            this.lstExecutions.Size = new System.Drawing.Size(264, 316);
            this.lstExecutions.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Best Bid";
            // 
            // lblBestBid
            // 
            this.lblBestBid.AutoSize = true;
            this.lblBestBid.Location = new System.Drawing.Point(278, 34);
            this.lblBestBid.Name = "lblBestBid";
            this.lblBestBid.Size = new System.Drawing.Size(0, 13);
            this.lblBestBid.TabIndex = 6;
            // 
            // lblBestBidSize
            // 
            this.lblBestBidSize.AutoSize = true;
            this.lblBestBidSize.Location = new System.Drawing.Point(278, 57);
            this.lblBestBidSize.Name = "lblBestBidSize";
            this.lblBestBidSize.Size = new System.Drawing.Size(0, 13);
            this.lblBestBidSize.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Best Bid Size";
            // 
            // lblLastTradedPrice
            // 
            this.lblLastTradedPrice.AutoSize = true;
            this.lblLastTradedPrice.Location = new System.Drawing.Point(278, 82);
            this.lblLastTradedPrice.Name = "lblLastTradedPrice";
            this.lblLastTradedPrice.Size = new System.Drawing.Size(0, 13);
            this.lblLastTradedPrice.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Last Traded Price";
            // 
            // lblBestAsk
            // 
            this.lblBestAsk.AutoSize = true;
            this.lblBestAsk.Location = new System.Drawing.Point(278, 104);
            this.lblBestAsk.Name = "lblBestAsk";
            this.lblBestAsk.Size = new System.Drawing.Size(0, 13);
            this.lblBestAsk.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Best Ask";
            // 
            // lblBestAskSize
            // 
            this.lblBestAskSize.AutoSize = true;
            this.lblBestAskSize.Location = new System.Drawing.Point(278, 127);
            this.lblBestAskSize.Name = "lblBestAskSize";
            this.lblBestAskSize.Size = new System.Drawing.Size(0, 13);
            this.lblBestAskSize.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Best Ask Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Executions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Ticker";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Balances";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Order Book";
            // 
            // lstOrderBookBids
            // 
            this.lstOrderBookBids.FormattingEnabled = true;
            this.lstOrderBookBids.Location = new System.Drawing.Point(15, 201);
            this.lstOrderBookBids.Name = "lstOrderBookBids";
            this.lstOrderBookBids.Size = new System.Drawing.Size(120, 134);
            this.lstOrderBookBids.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Bids";
            // 
            // lstOrderBookAsks
            // 
            this.lstOrderBookAsks.FormattingEnabled = true;
            this.lstOrderBookAsks.Location = new System.Drawing.Point(190, 201);
            this.lstOrderBookAsks.Name = "lstOrderBookAsks";
            this.lstOrderBookAsks.Size = new System.Drawing.Size(120, 134);
            this.lstOrderBookAsks.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(187, 185);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Asks";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 356);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Direction:";
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Location = new System.Drawing.Point(74, 356);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(0, 13);
            this.lblDirection.TabIndex = 24;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 580);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lstOrderBookAsks);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lstOrderBookBids);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBestAskSize);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblBestAsk);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblLastTradedPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblBestBidSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblBestBid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstExecutions);
            this.Controls.Add(this.lblBalanceBTCAmount);
            this.Controls.Add(this.lblBalanceBTC);
            this.Controls.Add(this.lblBalanceJPYAmount);
            this.Controls.Add(this.lblBalanceJPY);
            this.Name = "MainForm";
            this.Text = "Salamander";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBalanceJPY;
        private System.Windows.Forms.Label lblBalanceJPYAmount;
        private System.Windows.Forms.Label lblBalanceBTC;
        private System.Windows.Forms.Label lblBalanceBTCAmount;
        private System.Windows.Forms.ListBox lstExecutions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBestBid;
        private System.Windows.Forms.Label lblBestBidSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLastTradedPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBestAsk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBestAskSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstOrderBookBids;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lstOrderBookAsks;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblDirection;
    }
}

