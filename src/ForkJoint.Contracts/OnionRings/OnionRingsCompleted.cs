using ForkJoint.Contracts.OrderLine;

namespace ForkJoint.Contracts.OnionRings;

public interface OnionRingsCompleted :
    OrderLineCompleted
{
    int Quantity { get; }
}