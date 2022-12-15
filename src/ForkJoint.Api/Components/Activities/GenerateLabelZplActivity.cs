using System;
using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Components.Activities;

using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.ZplGenerator;

public class GenerateLabelZplActivity :
    IActivity<GenerateLabelZplArguments, GenerateLabelZplLog>
{
    private readonly IGenerateZpl _zplGenerator;
    private readonly ILogger<GenerateLabelZplActivity> _logger;

    public GenerateLabelZplActivity(ILogger<GenerateLabelZplActivity> logger, IGenerateZpl zplGenerator)
    {
        _logger = logger;
        _zplGenerator = zplGenerator;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<GenerateLabelZplArguments> context)
    {
        var zpl = await _zplGenerator.Generate(context.Arguments.LegData);
        
        var leg = new Leg
        {
            LegId = Guid.NewGuid(),
            LabelData = zpl.Data,
            Invoice = context.Arguments.Invoice,
            Labels = context.Arguments.Labels,
            Receipt = context.Arguments.Receipt
        };
        
        return context.CompletedWithVariables<GenerateLabelZplLog>(new {leg}, new {leg});
    }

    public Task<CompensationResult> Compensate(CompensateContext<GenerateLabelZplLog> context)
    {
        var zpl = context.Log.ZplLabel;

        _logger.LogDebug("Putting zpl label back in inventory: {zpl}", zpl);

        _zplGenerator.Add(zpl);

        return Task.FromResult(context.Compensated());
    }
}