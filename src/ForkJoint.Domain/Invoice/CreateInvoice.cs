using System;

namespace ForkJoint.Domain.Invoice;

public interface RequestInvoiceGeneration : ShipmentLine.ShipmentLine
{
    int Quantity { get; }
}

public interface CreateInvoice
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }
    int Quantity { get; }
}