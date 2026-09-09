using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Queue
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
                DisplayCashierQueue(cashierQueue.CashierQueue);

                MessageBox.Show("Cashier Queue Manager Loaded Successfully!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading form: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                MessageBox.Show("Cashier Queue Manager Closed Successfully!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing form: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayCashierQueue(cashierQueue.CashierQueue);
                MessageBox.Show("Queue Refreshed! Current Count: " + cashierQueue.QueueCount, 
                                "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing queue: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (listCashierQueue.Items.Count > 0)
                {
                    string currentTicket = cashierQueue.GetNextTicket();
                    cashierQueue.RemoveFromQueue(0); // Remove the first item (already done)
                    DisplayCashierQueue(cashierQueue.CashierQueue);
                    MessageBox.Show("Ticket " + currentTicket + " has been served and removed!", 
                                    "Next Ticket", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Queue is empty!", "Information", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing next ticket: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayCashierQueue(IEnumerable CashierList)
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
                MessageBox.Show("Error displaying queue: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Automatically refresh the list when there are updates in the queue
                if (cashierQueue.HasUpdates())
                {
                    DisplayCashierQueue(cashierQueue.CashierQueue);
                    System.Diagnostics.Debug.WriteLine("Auto-refresh at: " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in timer tick: " + ex.Message);
            }
        }
    }
}
