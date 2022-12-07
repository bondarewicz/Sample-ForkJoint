using System;
using ForkJoint.Domain.Future;

namespace ForkJoint.Domain.OrderLine;

public interface OrderLineCompleted :
    FutureCompleted
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    string Description { get; }
}