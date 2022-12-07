namespace ForkJoint.Contracts.OnionRings;

public interface OrderOnionRings :
    OrderLine.OrderLine
{
    int Quantity { get; }
}