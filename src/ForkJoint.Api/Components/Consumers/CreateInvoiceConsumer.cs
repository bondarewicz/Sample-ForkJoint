using System.Threading.Tasks;
using ForkJoint.Api.Services.InvoiceGenerator;
using ForkJoint.Api.Services.ReceiptGenerator;
using ForkJoint.Domain.Invoice;
using ForkJoint.Domain.Receipt;
using MassTransit;

namespace ForkJoint.Api.Components.Consumers;

public class CreateInvoiceConsumer :
    IConsumer<CreateInvoice>
{
    readonly IGenerateInvoice _generator;

    public CreateInvoiceConsumer(IGenerateInvoice generator)
    {
        _generator = generator;
    }

    public async Task Consume(ConsumeContext<CreateInvoice> context)
    {
        var invoice = await _generator.Generate(context.Message.Quantity);

        await context.RespondAsync<InvoiceReady>(new
        {
            context.Message.ShipmentId,
            context.Message.ShipmentLineId,
            context.Message.Quantity,
            InvoiceZpl = invoice.Data
        });
    }
}