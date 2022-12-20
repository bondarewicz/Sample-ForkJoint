using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Receipt;

public interface ReceiptCompleted : ShipmentLineCompleted
{
    string Receipt { get;  }
}