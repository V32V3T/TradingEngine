using System;

namespace TradingEngineServer.Orders
{
    public interface IOrderCore
    {
        public long OrderId { get; }
        public string UserName { get; }
        public int SecurityId { get; }

    }
}
