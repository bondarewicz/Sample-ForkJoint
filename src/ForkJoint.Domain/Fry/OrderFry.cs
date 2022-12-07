namespace ForkJoint.Domain.Fry;

public interface OrderFry :
    OrderLine.OrderLine
{
    Size Size { get; }
}