using System;
using MassTransit;

namespace ForkJoint.Domain.OrderLine;

[ExcludeFromTopology]
public interface OrderLine
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
}