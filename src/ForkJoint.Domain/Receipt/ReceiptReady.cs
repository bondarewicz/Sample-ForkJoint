using System;

namespace ForkJoint.Domain.Receipt;

public interface ReceiptReady
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }

    string ReceiptZpl { get; }
    int Quantity { get; }
}