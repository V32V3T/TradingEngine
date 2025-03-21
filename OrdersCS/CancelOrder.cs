using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class CancelOrder : IOrderCore
    {
        public CancelOrder(IOrderCore orderCore)
        {
            //FIELDS//
            _orderCore = orderCore;
        }

        //PROPERTIES//
        public long OrderId => _orderCore.OrderId;

        public string UserName => _orderCore.UserName;

        public int SecurityId => _orderCore.SecurityId;


        //FIELDS//
        private readonly IOrderCore _orderCore;

        
    }
}
