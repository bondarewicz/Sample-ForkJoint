namespace ForkJoint.Domain.Shake;

public interface OrderShake :
    OrderLine.OrderLine
{
    string Flavor { get; }
    Size Size { get; }
}