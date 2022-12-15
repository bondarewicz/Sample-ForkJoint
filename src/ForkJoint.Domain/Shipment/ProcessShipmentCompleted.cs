using System;
using System.Collections.Generic;
using ForkJoint.Domain.Future;
using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipmentCompleted : FutureCompleted
{
    Guid ShipmentId { get; }

    IDictionary<Guid, ShipmentLineCompleted> LinesCompleted { get; }
}