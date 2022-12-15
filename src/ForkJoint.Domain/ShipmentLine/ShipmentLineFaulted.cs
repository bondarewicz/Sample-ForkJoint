using System;
using ForkJoint.Domain.Future;

namespace ForkJoint.Domain.ShipmentLine;

public interface ShipmentLineFaulted : FutureFaulted
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }
    string Description { get; }
}