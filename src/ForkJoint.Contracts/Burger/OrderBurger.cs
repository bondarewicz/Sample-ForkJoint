namespace ForkJoint.Contracts.Burger;

public interface OrderBurger :
    OrderLine.OrderLine
{
    Contracts.Burger.Burger Burger { get; }
}