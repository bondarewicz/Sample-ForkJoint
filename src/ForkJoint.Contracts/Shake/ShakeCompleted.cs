using ForkJoint.Contracts.OrderLine;

namespace ForkJoint.Contracts.Shake;

public interface ShakeCompleted :
    OrderLineCompleted
{
    string Flavor { get; }
    Size Size { get; }
}