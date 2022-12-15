using System;
using System.Collections.Generic;
using ForkJoint.Domain.Future;
using ForkJoint.Domain.ShipmentLine;
using MassTransit;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipmentFaulted : FutureFaulted
{
    Guid ShipmentId { get; }

    IDictionary<Guid, ShipmentLineCompleted> LinesCompleted { get; }

    IDictionary<Guid, Fault<ShipmentLine.ShipmentLine>> LinesFaulted { get; }
}