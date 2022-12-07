namespace ForkJoint.Domain.Burger;

public interface OrderBurger :
    OrderLine.OrderLine
{
    Burger Burger { get; }
}