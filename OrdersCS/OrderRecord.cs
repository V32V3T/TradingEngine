using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders

{
    /// <summary>
    /// Read only  representation of an Order
    /// </summary>
    public record OrderRecord(long OrderId, uint Quantity, long Price, 
        bool isBuySide, string Username, int SecurityId, uint TheoreticalQueuePosition);
    
}
/// <summary>
/// for adding record types
/// </summary>
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { };
}
