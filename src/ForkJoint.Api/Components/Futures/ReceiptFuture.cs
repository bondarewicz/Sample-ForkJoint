namespace ForkJoint.Api.Components.Futures;

using MassTransit;
using ForkJoint.Domain.Receipt;

public class ReceiptFuture :
    Future<CreateReceipt, ReceiptCompleted>
{
    public ReceiptFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentId));

        SendRequest<CreateReceipt>()
            .OnResponseReceived<ReceiptReady>(x =>
            {
                x.SetCompletedUsingInitializer(context => new { Description = $"{context.Message.Quantity} Receipt" });
            });
    }
}