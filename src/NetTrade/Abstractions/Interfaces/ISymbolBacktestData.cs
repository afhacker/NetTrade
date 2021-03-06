﻿using System;
using System.Collections.Generic;

namespace NetTrade.Abstractions.Interfaces
{
    public interface ISymbolBacktestData : ICloneable
    {
        ISymbol Symbol { get; }

        IReadOnlyList<IBar> Data { get; }

        IBar GetBar(DateTimeOffset time);

        IBar GetNearestBar(DateTimeOffset time);
    }
}