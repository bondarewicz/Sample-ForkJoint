using System.Threading.Tasks;
using ForkJoint.Api.Services.ReceiptGenerator;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Components.Activities;

public class GenerateReceiptActivity : IActivity<GenerateReceiptArguments, GenerateReceiptLog>
{
    private readonly IGenerateReceipt _generator;
    private readonly ILogger<GenerateReceiptActivity> _logger;

    public GenerateReceiptActivity(IGenerateReceipt generator, ILogger<GenerateReceiptActivity> logger)
    {
        _generator = generator;
        _logger = logger;
    }
    
    public async Task<ExecutionResult> Execute(ExecuteContext<GenerateReceiptArguments> context)
    {
        var zplReceipt = await _generator.Generate(context.Arguments.Data);
        
        // var leg = new Leg
        // {
        //     LegId = Guid.NewGuid(),
        //     LegData = context.Arguments.LegData,
        //     LabelData = zpl.Data,
        //     Invoice = context.Arguments.Invoice,
        //     Labels = context.Arguments.Labels,
        //     Receipt = context.Arguments.Receipt
        // };
        
        return context.CompletedWithVariables<GenerateReceiptLog>(new {zplReceipt}, new {zplReceipt});
    }

    public Task<CompensationResult> Compensate(CompensateContext<GenerateReceiptLog> context)
    {
        throw new System.NotImplementedException();
    }
}