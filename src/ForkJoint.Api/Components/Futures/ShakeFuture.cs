namespace ForkJoint.Api.Components.Futures;

using MassTransit;
using Domain.Shake;

public class ShakeFuture :
    Future<OrderShake, ShakeCompleted>
{
    public ShakeFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.OrderLineId));

        SendRequest<PourShake>()
            .OnResponseReceived<ShakeReady>(x =>
                x.SetCompletedUsingInitializer(context => new { Description = $"{context.Message.Size} {context.Message.Flavor} Shake" }));
    }
}