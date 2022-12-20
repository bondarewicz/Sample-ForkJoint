using System;

namespace ForkJoint.Domain.Receipt;

public interface RequestReceiptGeneration : ShipmentLine.ShipmentLine
{
    int Quantity { get; }
}

public interface CreateReceipt 
{
    Guid ShipmentId { get; }
    Guid ShipmentLineId { get; }
    int Quantity { get; }
}