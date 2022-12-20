using System;
using System.Collections.Generic;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipment
{
    Guid ShipmentId { get; }
    IEnumerable<Leg.Leg> Legs { get; }
    
}