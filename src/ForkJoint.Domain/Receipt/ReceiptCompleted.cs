using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Receipt;

public interface ReceiptCompleted : ShipmentLineCompleted
{
    int Quantity { get; }
}