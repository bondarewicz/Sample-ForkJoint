namespace ForkJoint.Api.Components.Futures;

using MassTransit;
using Contracts.Burger;

public class BurgerFuture :
    Future<OrderBurger, BurgerCompleted>
{
    public BurgerFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderLineId));

        ExecuteRoutingSlip(x => x
            .OnRoutingSlipCompleted(r => r
                .SetCompletedUsingInitializer(context =>
                {
                    var burger = context.GetVariable<Burger>(nameof(BurgerCompleted.Burger));

                    return new
                    {
                        Burger = burger,
                        Description = burger!.ToString()
                    };
                })));
    }
}