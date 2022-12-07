namespace ForkJoint.Domain.Burger;

public record BurgerPatty
{
    public decimal Weight { get; init; }
    public bool Cheese { get; init; }
}