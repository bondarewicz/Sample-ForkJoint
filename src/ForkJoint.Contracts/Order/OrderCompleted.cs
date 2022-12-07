using System;
using System.Collections.Generic;
using ForkJoint.Contracts.Future;
using ForkJoint.Contracts.OrderLine;

namespace ForkJoint.Contracts.Order;

public interface OrderCompleted :
    FutureCompleted
{
    Guid OrderId { get; }

    IDictionary<Guid, OrderLineCompleted> LinesCompleted { get; }
}