using System;
using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Components.Activities;

using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.ZplGenerator;

public class GenerateLabelActivity :
    IActivity<GenerateLabelArguments, GenerateLabelLog>
{
    private readonly IGenerateLabels _generator;
    private readonly ILogger<GenerateLabelActivity> _logger;

    public GenerateLabelActivity(ILogger<GenerateLabelActivity> logger, IGenerateLabels generator)
    {
        _logger = logger;
        _generator = generator;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<GenerateLabelArguments> context)
    {
        var zpl = await _generator.Generate(context.Arguments.LegData);
        
        var leg = new Leg
        {
            LegId = Guid.NewGuid(),
            LegData = context.Arguments.LegData,
            LabelData = zpl.Data,
            Invoice = context.Arguments.Invoice,
            Labels = context.Arguments.Labels,
            Receipt = context.Arguments.Receipt
        };
        
        return context.CompletedWithVariables<GenerateLabelLog>(new {leg}, new {leg});
    }

    public Task<CompensationResult> Compensate(CompensateContext<GenerateLabelLog> context)
    {
        var zpl = context.Log.ZplLabel;

        _logger.LogDebug("Putting zpl label back in inventory: {zpl}", zpl);

        _generator.Add(zpl);

        return Task.FromResult(context.Compensated());
    }
}