using System;
using MassTransit;

namespace ForkJoint.Domain.ShipmentLine;

[ExcludeFromTopology]
public interface ShipmentLine
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }
}