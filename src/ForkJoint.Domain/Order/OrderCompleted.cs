using System;
using System.Collections.Generic;
using ForkJoint.Domain.Future;
using ForkJoint.Domain.OrderLine;

namespace ForkJoint.Domain.Order;

public interface OrderCompleted :
    FutureCompleted
{
    Guid OrderId { get; }

    IDictionary<Guid, OrderLineCompleted> LinesCompleted { get; }
}