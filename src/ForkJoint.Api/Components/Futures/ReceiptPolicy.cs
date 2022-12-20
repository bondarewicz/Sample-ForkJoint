namespace ForkJoint.Api.Components.Futures;

using MassTransit;
using ForkJoint.Domain.Receipt;

public class ReceiptPolicy :
    Future<RequestReceiptGeneration, ReceiptCompleted>
{
    public ReceiptPolicy()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentLineId));
        
        SendRequest<CreateReceipt>()
            .OnResponseReceived<ReceiptReady>(x =>
                x.SetCompletedUsingInitializer(context => new { Description = $"Receipt: {context.Message.ReceiptZpl}" }));
    }
}