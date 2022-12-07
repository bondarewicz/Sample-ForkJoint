using System;
using ForkJoint.Contracts.Future;

namespace ForkJoint.Contracts.OrderLine;

public interface OrderLineCompleted :
    FutureCompleted
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    string Description { get; }
}