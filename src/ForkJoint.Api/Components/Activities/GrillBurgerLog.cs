using ForkJoint.Contracts.Burger;

namespace ForkJoint.Api.Components.Activities;

using Contracts;


public interface GrillBurgerLog
{
    BurgerPatty Patty { get; }
}