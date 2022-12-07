using ForkJoint.Domain.OrderLine;

namespace ForkJoint.Domain.Burger;

public interface BurgerCompleted :
    OrderLineCompleted
{
    Burger Burger { get; }
}