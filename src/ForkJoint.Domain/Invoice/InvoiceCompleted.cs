using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Invoice;

public interface InvoiceCompleted : ShipmentLineCompleted
{
    string Invoice { get;  }
}