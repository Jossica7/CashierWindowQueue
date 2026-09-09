using System;
using System.Collections;
using System.Windows.Forms;

namespace CashierWindowQueue
{
    public partial class CashierWindowQueueForm : Form
    {
        private CashierClass cashierQueue;
        private Timer timer;

        public CashierWindowQueueForm()
        {
            InitializeComponent();
            cashierQueue = new CashierClass();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.listCashierQueue = new System.Windows.Forms.ListView();
            this.SuspendLayout();

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(12, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnNext
            this.btnNext.Location = new System.Drawing.Point(118, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 30);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // listCashierQueue
            this.listCashierQueue.Location = new System.Drawing.Point(12, 50);
            this.listCashierQueue.Name = "listCashierQueue";
            this.listCashierQueue.Size = new System.Drawing.Size(260, 200);
            this.listCashierQueue.TabIndex = 2;
            this.listCashierQueue.UseCompatibleStateImageBehavior = false;
            this.listCashierQueue.View = System.Windows.Forms.View.List;

            // CashierWindowQueueForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.listCashierQueue);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRefresh);
            this.Name = "CashierWindowQueueForm";
            this.Text = "Cashier Queue Manager";
            this.ResumeLayout(false);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayCashierQueue(cashierQueue.CashierList);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (listCashierQueue.Items.Count > 0)
            {
                // Remove the first item from the queue
                cashierQueue.RemoveFromQueue(0);
                DisplayCashierQueue(cashierQueue.CashierList);
            }
            else
            {
                MessageBox.Show("Queue is empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DisplayCashierQueue(IEnumerable CashierList)
        {
            listCashierQueue.Items.Clear();
            foreach (object obj in CashierList)
            {
                listCashierQueue.Items.Add(obj.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Automatically refresh the list when there are updates
            if (cashierQueue.HasUpdates())
            {
                DisplayCashierQueue(cashierQueue.CashierList);
            }
        }

        private Button btnRefresh;
        private Button btnNext;
        private ListView listCashierQueue;
    }
}
