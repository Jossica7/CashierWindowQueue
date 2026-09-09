using System;
using System.Collections;
using System.Collections.Generic;

namespace CashierWindowQueue
{
    public class CashierClass
    {
        private List<string> cashierList;
        private bool hasUpdates;

        public CashierClass()
        {
            cashierList = new List<string>();
            InitializeQueue();
            hasUpdates = false;
        }

        private void InitializeQueue()
        {
            // Initialize with sample queue numbers
            cashierList.Add("P - 10001");
            cashierList.Add("P - 10002");
            cashierList.Add("P - 10003");
            cashierList.Add("P - 10004");
            cashierList.Add("P - 10005");
            cashierList.Add("P - 10006");
            cashierList.Add("P - 10007");
        }

        public IEnumerable CashierQueue
        {
            get { return cashierList; }
        }

        public List<string> CashierList
        {
            get { return cashierList; }
        }

        public void AddToQueue(string ticketNumber)
        {
            cashierList.Add(ticketNumber);
            hasUpdates = true;
        }

        public void RemoveFromQueue(int index)
        {
            if (index >= 0 && index < cashierList.Count)
            {
                cashierList.RemoveAt(index);
                hasUpdates = true;
            }
        }

        public void RemoveFromQueue(string ticketNumber)
        {
            if (cashierList.Contains(ticketNumber))
            {
                cashierList.Remove(ticketNumber);
                hasUpdates = true;
            }
        }

        public bool HasUpdates()
        {
            if (hasUpdates)
            {
                hasUpdates = false;
                return true;
            }
            return false;
        }

        public string GetNextTicket()
        {
            if (cashierList.Count > 0)
            {
                return cashierList[0];
            }
            return null;
        }

        public int QueueCount
        {
            get { return cashierList.Count; }
        }

        public void ClearQueue()
        {
            cashierList.Clear();
            hasUpdates = true;
        }
    }
}
