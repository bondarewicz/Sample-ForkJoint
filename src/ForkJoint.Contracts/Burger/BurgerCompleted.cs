using ForkJoint.Contracts.OrderLine;

namespace ForkJoint.Contracts.Burger;

public interface BurgerCompleted :
    OrderLineCompleted
{
    Burger Burger { get; }
}