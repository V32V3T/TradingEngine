using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    
    public class OrderBookEntry
    {
        public OrderBookEntry(Order currentOrder, Limit parentLimit )
        {
            CreationTime = DateTime.Now;
            ParentLimit = parentLimit;
            CurrentOrder = currentOrder;

        }
        public DateTime CreationTime { get; private set; }
        public Order CurrentOrder { get; private set; }
        public Limit ParentLimit { get; private set; }

        public OrderBookEntry Next { get; set; }
        public OrderBookEntry Previous { get; set; }

    }
}
