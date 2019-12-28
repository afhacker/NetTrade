﻿using NetTrade.Helpers;
using NetTrade.Implementations;
using NetTrade.Interfaces;
using System;

namespace NetTrade.Abstractions
{
    public abstract class Bars : IBars
    {
        private ExpandableSeries<DateTimeOffset> _time = new ExpandableSeries<DateTimeOffset>();

        private ExpandableSeries<double> _open = new ExpandableSeries<double>();

        private ExpandableSeries<double> _high = new ExpandableSeries<double>();

        private ExpandableSeries<double> _low = new ExpandableSeries<double>();

        private ExpandableSeries<double> _close = new ExpandableSeries<double>();

        private ExpandableSeries<long> _volume = new ExpandableSeries<long>();

        public ISeries<DateTimeOffset> Time => _time;

        public ISeries<double> Open => _open;

        public ISeries<double> High => _high;

        public ISeries<double> Low => _low;

        public ISeries<double> Close => _close;

        public ISeries<long> Volume => _volume;

        public event OnBarHandler OnBarEvent;

        public virtual int AddBar(IBar bar)
        {
            int index = _time.Count;

            _time.Add(index, bar.Time);
            _open.Add(index, bar.Open);
            _high.Add(index, bar.High);
            _low.Add(index, bar.Low);
            _volume.Add(index, bar.Volume);

            OnBarEvent?.Invoke(this, index);

            return index;
        }

        public abstract object Clone();
    }
}