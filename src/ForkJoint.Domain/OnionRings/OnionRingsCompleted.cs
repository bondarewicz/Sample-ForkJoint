using ForkJoint.Domain.OrderLine;

namespace ForkJoint.Domain.OnionRings;

public interface OnionRingsCompleted :
    OrderLineCompleted
{
    int Quantity { get; }
}