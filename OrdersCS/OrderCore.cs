using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Orders
{
    public class OrderCore : IOrderCore
    {
        public OrderCore(long orderId, string userName, int securityId)
        {
            OrderId = orderId;
            UserName = userName;
            SecurityId = securityId;
        }

        public long OrderId { get; private set; }
        public string UserName { get; private set; }
        public int SecurityId { get; private set; } 

    }
}
