using System;
using System.Collections.Generic;
using ForkJoint.Domain.Future;
using ForkJoint.Domain.OrderLine;
using MassTransit;

namespace ForkJoint.Domain.Order;

public interface OrderFaulted :
    FutureFaulted
{
    Guid OrderId { get; }

    IDictionary<Guid, OrderLineCompleted> LinesCompleted { get; }

    IDictionary<Guid, Fault<OrderLine.OrderLine>> LinesFaulted { get; }
}