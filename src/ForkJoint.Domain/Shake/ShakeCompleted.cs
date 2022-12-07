using ForkJoint.Domain.OrderLine;

namespace ForkJoint.Domain.Shake;

public interface ShakeCompleted :
    OrderLineCompleted
{
    string Flavor { get; }
    Size Size { get; }
}