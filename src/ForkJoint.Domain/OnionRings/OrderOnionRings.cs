namespace ForkJoint.Domain.OnionRings;

public interface OrderOnionRings :
    OrderLine.OrderLine
{
    int Quantity { get; }
}