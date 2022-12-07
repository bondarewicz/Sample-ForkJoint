using ForkJoint.Domain.OrderLine;

namespace ForkJoint.Domain.FryShake;

public interface FryShakeCompleted :
    OrderLineCompleted
{
    string Flavor { get; }
    Size Size { get; }
}


public interface ComboCompleted :
    OrderLineCompleted
{
}