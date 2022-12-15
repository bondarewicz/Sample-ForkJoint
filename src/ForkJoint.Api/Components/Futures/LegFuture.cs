using ForkJoint.Api.Components.Activities;
using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Components.Futures;

using MassTransit;

public class LegFuture :
    Future<OrderLeg, LegCompleted>
{
    public LegFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentLineId));

        ExecuteRoutingSlip(x => x
            .OnRoutingSlipCompleted(r => r
                .SetCompletedUsingInitializer(context =>
                {
                    var leg = context.GetVariable<Leg>(nameof(LegCompleted.Leg));

                    return new
                    {
                        Leg = leg,
                        Description = leg!.ToString()
                    };
                })));
    }
}