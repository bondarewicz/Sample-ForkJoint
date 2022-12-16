// using System.Threading.Tasks;
// using ForkJoint.Api.Services.ReceiptGenerator;
// using ForkJoint.Domain.Receipt;
// using MassTransit;
//
// namespace ForkJoint.Api.Components.Consumers;
//
// public class CreateReceiptConsumer :
//     IConsumer<CreateReceipt>
// {
//     readonly IGenerateReceipt _generator;
//
//     public CreateReceiptConsumer(IGenerateReceipt generator)
//     {
//         _generator = generator;
//     }
//
//     public async Task Consume(ConsumeContext<CreateReceipt> context)
//     {
//         var receipt = await _generator.Generate(context.Message.ReceiptData);
//
//         await context.RespondAsync<ReceiptReady>(new
//         {
//             context.Message.ShipmentId,
//             context.Message.ShipmentLineId,
//             context.Message.Quantity,
//             ReceiptZpl = receipt.Data
//         });
//     }
// }