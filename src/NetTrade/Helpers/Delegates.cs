﻿using NetTrade.Interfaces;
using System;

namespace NetTrade.Helpers
{
    public delegate void OnBarHandler(object sender, int index);

    public delegate void OnTickHandler(object sender);

    public delegate void OnBacktestStartHandler(object sender, IRobot robot);

    public delegate void OnBacktestStopHandler(object sender, IRobot robot);

    public delegate void OnBacktestPauseHandler(object sender, IRobot robot);

    public delegate void OnOptimizationPassCompletionHandler(object sender, IRobot robot);

    public delegate void OnOptimizationStartedHandler(object sender);

    public delegate void OnOptimizationStoppedHandler(object sender);

    public delegate void OnEquityChangedHandler(object sender, double amount, DateTimeOffset time);

    public delegate void OnBalanceChangedHandler(object sender, double amount, DateTimeOffset time);

    public delegate void OnMarginChangedHandler(object sender, double amount, DateTimeOffset time);

    public delegate void OnMarginCallHandler(object sender);
}