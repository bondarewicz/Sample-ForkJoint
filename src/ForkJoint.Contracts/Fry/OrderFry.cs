namespace ForkJoint.Contracts.Fry;

public interface OrderFry :
    OrderLine.OrderLine
{
    Size Size { get; }
}