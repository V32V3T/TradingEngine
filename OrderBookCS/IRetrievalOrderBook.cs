using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Orderbook
{
    public interface IRetrievalOrderBook : IOrderEntryOrderbook
    {
        List<OrderBookEntry> GetAskOrders();
        List<OrderBookEntry> GetBidOrders();
    }
}
