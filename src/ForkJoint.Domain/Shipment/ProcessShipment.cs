using System;
using System.Collections.Generic;

namespace ForkJoint.Domain.Shipment;

public interface ProcessShipment
{
    Guid ShipmentId { get; }
    IEnumerable<Label.Label> Labels { get; }
    IEnumerable<Invoice.Invoice> Invoices { get; }
}