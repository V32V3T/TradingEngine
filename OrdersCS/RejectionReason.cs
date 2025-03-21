using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders

{
    public enum RejectionReason
    {
        Unknown,
        OrderNotFound,
        InstrumentNotFound,
        AttemptingToModifyWrongSide
    }
}
