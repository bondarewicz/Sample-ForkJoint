using System;

namespace ForkJoint.Domain.Receipt;

public interface CreateReceipt
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }

    string ReceiptData { get; }
    int Quantity { get; }
}