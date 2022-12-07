using System;
using System.Collections.Generic;
using ForkJoint.Contracts.Future;
using ForkJoint.Contracts.OrderLine;
using MassTransit;

namespace ForkJoint.Contracts.Order;

public interface OrderFaulted :
    FutureFaulted
{
    Guid OrderId { get; }

    IDictionary<Guid, OrderLineCompleted> LinesCompleted { get; }

    IDictionary<Guid, Fault<OrderLine.OrderLine>> LinesFaulted { get; }
}