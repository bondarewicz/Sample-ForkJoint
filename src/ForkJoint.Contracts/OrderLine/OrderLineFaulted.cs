using System;
using ForkJoint.Contracts.Future;

namespace ForkJoint.Contracts.OrderLine;

public interface OrderLineFaulted :
    FutureFaulted
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    string Description { get; }
}