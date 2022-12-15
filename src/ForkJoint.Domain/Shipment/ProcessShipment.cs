using System;
using System.Collections.Generic;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipment : ShipmentLine.ShipmentLine
{
    Guid ShipmentId { get; }
    IEnumerable<Leg.Leg> Legs { get; }
}