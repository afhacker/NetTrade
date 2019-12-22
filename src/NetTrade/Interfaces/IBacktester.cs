﻿using NetTrade.Helpers;
using System;

namespace NetTrade.Interfaces
{
    public interface IBacktester
    {
        IRobot Robot { get; }

        event OnBacktestStartHandler OnBacktestStartEvent;

        event OnBacktestPauseHandler OnBacktestPauseEvent;

        event OnBacktestStopHandler OnBacktestStopEvent;

        void Start(IRobot robot, IBacktestSettings settings);

        void Pause();

        void Stop();

        IBacktestResult GetResult();
    }
}