using System;
using MassTransit;

namespace ForkJoint.Contracts.OrderLine;

[ExcludeFromTopology]
public interface OrderLine
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
}