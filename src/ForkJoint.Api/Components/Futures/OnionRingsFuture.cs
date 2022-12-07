namespace ForkJoint.Api.Components.Futures;

using MassTransit;
using Contracts.OnionRings;

public class OnionRingsFuture :
    Future<OrderOnionRings, OnionRingsCompleted>
{
    public OnionRingsFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderLineId));

        SendRequest<CookOnionRings>()
            .OnResponseReceived<OnionRingsReady>(x =>
            {
                x.SetCompletedUsingInitializer(context => new { Description = $"{context.Message.Quantity} Onion Rings" });
            });
    }
}