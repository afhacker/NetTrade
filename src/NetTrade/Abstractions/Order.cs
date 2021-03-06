﻿using NetTrade.Abstractions.Interfaces;
using NetTrade.Enums;
using System;

namespace NetTrade.Abstractions
{
    public abstract class Order : IOrder
    {
        public Order(IOrderParameters orderParameters, DateTimeOffset openTime)
        {
            Symbol = orderParameters.Symbol;

            TradeType = orderParameters.TradeType;

            OrderType = orderParameters.OrderType;

            Volume = orderParameters.Volume;

            Comment = orderParameters.Comment;

            OpenTime = openTime;
        }

        public TradeType TradeType { get; }

        public OrderType OrderType { get; }

        public DateTimeOffset OpenTime { get; }

        public string Comment { get; }

        public double? StopLossPrice { get; set; }

        public double? TakeProfitPrice { get; set; }

        public double Volume { get; }

        public ISymbol Symbol { get; }
    }
}