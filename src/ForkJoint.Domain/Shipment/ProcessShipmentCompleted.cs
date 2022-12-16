using System;
using System.Collections.Generic;
using ForkJoint.Domain.Future;
using ForkJoint.Domain.Leg;
using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipmentCompleted : FutureCompleted
{
    Guid ShipmentId { get; }
    IDictionary<Guid, LegLabelCompleted> Labels { get; }
    IDictionary<Guid, ShipmentLineCompleted> LinesCompleted { get; }
}