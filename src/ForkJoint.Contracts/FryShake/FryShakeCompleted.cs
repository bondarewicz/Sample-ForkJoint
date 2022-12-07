using ForkJoint.Contracts.OrderLine;

namespace ForkJoint.Contracts.FryShake;

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