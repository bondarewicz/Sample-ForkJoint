using ForkJoint.Domain.Invoice;
using MassTransit;

namespace ForkJoint.Api.Components.Futures;

public class InvoiceFuture : 
    Future<RequestInvoiceGeneration, InvoiceCompleted>
{
    public InvoiceFuture()
    {
        ConfigureCommand(x => x.CorrelateById(context => context.Message.ShipmentLineId));
        
        SendRequest<CreateInvoice>()
            .OnResponseReceived<InvoiceReady>(x =>
                x.SetCompletedUsingInitializer(context => new { Description = $"Invoice: {context.Message.InvoiceZpl}" }));
    }
}