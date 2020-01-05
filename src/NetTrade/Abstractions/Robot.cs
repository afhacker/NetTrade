﻿using NetTrade.Abstractions.Interfaces;
using NetTrade.Enums;
using NetTrade.Models;
using System;

namespace NetTrade.Abstractions
{
    public abstract class Robot : IRobot
    {
        public Robot(IRobotSettings settings)
        {
            _ = settings ?? throw new ArgumentNullException(nameof(settings));

            Settings = settings;

            RunningMode = RunningMode.Stopped;
        }

        public IRobotSettings Settings { get; }

        public RunningMode RunningMode { get; private set; }

        public void Start()
        {
            if (RunningMode == RunningMode.Running || RunningMode == RunningMode.Paused)
            {
                throw new InvalidOperationException("The robot is already in running/paused mode");
            }

            Settings.MainSymbol.OnTickEvent += Symbol_OnTickEvent;
            Settings.MainSymbol.Bars.OnBarEvent += SymbolBars_OnBarEvent;

            foreach (var symbol in Settings.OtherSymbols)
            {
                symbol.OnTickEvent += Symbol_OnTickEvent;
                symbol.Bars.OnBarEvent += SymbolBars_OnBarEvent;
            }

            RunningMode = RunningMode.Running;

            OnStart();

            if (Settings.Mode == Mode.Backtest)
            {
                Backtest();
            }
        }

        public void Stop()
        {
            if (RunningMode == RunningMode.Stopped)
            {
                throw new InvalidOperationException("The robot is already stopped");
            }

            RunningMode = RunningMode.Stopped;

            OnStop();
        }

        public void Pause()
        {
            if (RunningMode != RunningMode.Running)
            {
                throw new InvalidOperationException("The robot is not running");
            }

            RunningMode = RunningMode.Paused;

            OnPause();
        }

        public void Resume()
        {
            if (RunningMode != RunningMode.Paused)
            {
                throw new InvalidOperationException("The robot is not paused");
            }

            RunningMode = RunningMode.Running;

            OnResume();
        }

        public void SetTimeByBacktester(IBacktester backtester, DateTimeOffset time)
        {
            _ = backtester ?? throw new ArgumentNullException(nameof(backtester));

            if (Settings.Mode == Mode.Live)
            {
                throw new InvalidOperationException("You can not set the robot time with a back tester when the robot is on" +
                    " live mode");
            }

            if (Settings.Backtester != backtester)
            {
                throw new InvalidOperationException("You can not set the robot time with another back tester, " +
                    "the provided back tester isn't the one available on robot settings");
            }

            (Settings.Server as Server).CurrentTime = time;
        }

        public virtual void OnTick(ISymbol symbol)
        {
        }

        public virtual void OnBar(ISymbol symbol, int index)
        {
        }

        public abstract void OnPause();

        public abstract void OnResume();

        public abstract void OnStart();

        public abstract void OnStop();

        #region Symbols on tick/bar event handlers

        private void Symbol_OnTickEvent(object sender)
        {
            var symbol = sender as ISymbol;

            Settings.TradeEngine.UpdateSymbolOrders(symbol);

            if (RunningMode == RunningMode.Running)
            {
                OnTick(symbol);
            }
        }

        private void SymbolBars_OnBarEvent(object sender, int index)
        {
            var symbol = sender as ISymbol;

            if (RunningMode == RunningMode.Running)
            {
                OnBar(symbol, index);
            }
        }

        #endregion Symbols on tick/bar event handlers

        #region Backtest methods and event handlers

        private void Backtest()
        {
            Settings.Backtester.OnBacktestStopEvent += Backtester_OnBacktestStopEvent;
            Settings.Backtester.OnBacktestStartEvent += Backtester_OnBacktestStartEvent;
            Settings.Backtester.OnBacktestPauseEvent += Backtester_OnBacktestPauseEvent;

            Settings.Backtester.Start(this, Settings.BacktestSettings);
        }

        protected virtual void Backtester_OnBacktestPauseEvent(object sender, IRobot robot)
        {
        }

        protected virtual void Backtester_OnBacktestStartEvent(object sender, IRobot robot)
        {
        }

        protected virtual void Backtester_OnBacktestStopEvent(object sender, IRobot robot)
        {
            if (RunningMode != RunningMode.Stopped)
            {
                Stop();
            }
        }

        #endregion
    }
}