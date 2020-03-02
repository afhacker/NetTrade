﻿using NetTrade.Enums;
using NetTrade.Models;
using System.Collections.Generic;

namespace NetTrade.Abstractions.Interfaces
{
    public interface ITradeEngine
    {
        IReadOnlyList<IOrder> Orders { get; }

        IReadOnlyList<ITrade> Trades { get; }

        IReadOnlyList<ITradingEvent> Journal { get; }

        IServer Server { get; }

        IAccount Account { get; }

        TradeResult Execute(IOrderParameters parameters);

        void UpdateSymbolOrders(ISymbol symbol);

        void CloseMarketOrder(MarketOrder order);

        void CloseMarketOrder(MarketOrder order, CloseReason closeReason);

        void CloseAllMarketOrders();

        void CloseAllMarketOrders(TradeType tradeType);

        void CancelPendingOrder(PendingOrder order);
    }
}