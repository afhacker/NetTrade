﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetTrade.Interfaces
{
    public interface ISeries<T>: IReadOnlyList<T>
    {
        T LastValue { get; }

        T Last(int index);
    }
}