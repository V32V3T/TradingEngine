using System;

namespace TradingEngineServer.Orderbook
{
    public interface IReadOnlyOrderbook
    {
        bool ContainsOrder(long orderId); //returns true if the orderbook contains an order with the given orderId
        OrderbookSpread GetSpread(); 
        int Count { get; }//returns the spread of the orderbook

    }
}
