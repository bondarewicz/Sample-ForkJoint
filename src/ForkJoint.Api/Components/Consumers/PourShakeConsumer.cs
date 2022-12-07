namespace ForkJoint.Api.Components.Consumers;

using System.Threading.Tasks;
using MassTransit;
using Services.ShakeMachine;
using Domain.Shake;

public class PourShakeConsumer :
    IConsumer<PourShake>
{
    readonly IShakeMachine _shakeMachine;

    public PourShakeConsumer(IShakeMachine shakeMachine)
    {
        _shakeMachine = shakeMachine;
    }

    public async Task Consume(ConsumeContext<PourShake> context)
    {
        await _shakeMachine.PourShake(context.Message.Flavor,
            context.Message.Size);

        await context.RespondAsync<ShakeReady>(new
        {
            context.Message.OrderId,
            context.Message.OrderLineId,
            context.Message.Flavor,
            context.Message.Size
        });
    }
}