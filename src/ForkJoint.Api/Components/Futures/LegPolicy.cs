using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Components.Futures;

using MassTransit;

public class LegPolicy :
    Future<RequestLabelGeneration, LegLabelCompleted>
{
    public LegPolicy()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentLineId));

        ExecuteRoutingSlip(x => x
            .OnRoutingSlipCompleted(r => r
                .SetCompletedUsingInitializer(context =>
                {
                    var leg = context.GetVariable<Leg>(nameof(LegLabelCompleted.Leg));

                    return new
                    {
                        Leg = leg,
                        Description = leg!.ToString()
                    };
                })));
    }
}