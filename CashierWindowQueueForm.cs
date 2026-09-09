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
            this.Load += new System.EventHandler(this.CashierWindowQueueForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CashierWindowQueueForm_FormClosing);
            this.ResumeLayout(false);
        }

        // Load Event - Initialize form and start timer
        private void CashierWindowQueueForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize Timer for automatic refresh
                timer = new Timer();
                timer.Interval = 1000; // 1 second
                timer.Tick += new EventHandler(timer1_Tick);
                timer.Start();

                // Display initial queue
                DisplayCashierQueue(cashierQueue.CashierList);

                MessageBox.Show("Cashier Queue Manager Loaded Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Closing Event - Clean up resources
        private void CashierWindowQueueForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Stop and dispose timer
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                }

                // Clear queue
                cashierQueue.ClearQueue();

                // Log closure
                System.Diagnostics.Debug.WriteLine("Cashier Queue Manager Form Closed at: " + DateTime.Now);

                MessageBox.Show("Cashier Queue Manager Closed Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayCashierQueue(cashierQueue.CashierList);
                MessageBox.Show("Queue Refreshed! Current Count: " + cashierQueue.QueueCount, "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing queue: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (listCashierQueue.Items.Count > 0)
                {
                    string currentTicket = cashierQueue.GetNextTicket();
                    cashierQueue.RemoveFromQueue(0);
                    DisplayCashierQueue(cashierQueue.CashierList);
                    MessageBox.Show("Ticket " + currentTicket + " has been served!", "Next Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Queue is empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing next ticket: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayCashierQueue(System.Collections.Generic.List<string> CashierList)
        {
            try
            {
                listCashierQueue.Items.Clear();
                foreach (object obj in CashierList)
                {
                    listCashierQueue.Items.Add(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying queue: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Automatically refresh the list when there are updates
                if (cashierQueue.HasUpdates())
                {
                    DisplayCashierQueue(cashierQueue.CashierList);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in timer tick: " + ex.Message);
            }
        }

        private Button btnRefresh;
        private Button btnNext;
        private ListView listCashierQueue;
    }
}
