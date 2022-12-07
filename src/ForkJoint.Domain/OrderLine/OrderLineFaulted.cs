using System;
using ForkJoint.Domain.Future;

namespace ForkJoint.Domain.OrderLine;

public interface OrderLineFaulted :
    FutureFaulted
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    string Description { get; }
}