using System;

namespace ForkJoint.Domain.Invoice;

public interface InvoiceReady
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }

    string InvoiceZpl { get; }
    int Quantity { get; }
}