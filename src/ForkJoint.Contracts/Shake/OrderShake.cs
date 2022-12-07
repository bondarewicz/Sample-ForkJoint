namespace ForkJoint.Contracts.Shake;

public interface OrderShake :
    OrderLine.OrderLine
{
    string Flavor { get; }
    Size Size { get; }
}