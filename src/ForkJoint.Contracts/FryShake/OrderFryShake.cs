namespace ForkJoint.Contracts.FryShake;

public interface OrderFryShake :
    OrderLine.OrderLine
{
    string Flavor { get; }
    Size Size { get; }
}


public interface OrderCombo :
    OrderLine.OrderLine
{
    int Number { get; }
}