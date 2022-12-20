using System;
using System.Threading.Tasks;
using ForkJoint.Api.Services.LabelsConverter;
using ForkJoint.Domain.Leg;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Components.Activities;

public class ConvertLabelActivity : 
    IActivity<ConvertLabelArguments, ConvertLabelLog>
{
    private readonly IConvertLabels _converter;
    private readonly ILogger<ConvertLabelActivity> _logger;
    
    public ConvertLabelActivity(ILogger<ConvertLabelActivity> logger, IConvertLabels converter)
    {
        _logger = logger;
        _converter = converter;
    }
    
    public async Task<ExecutionResult> Execute(ExecuteContext<ConvertLabelArguments> context)
    {
        var zpl = context.GetVariable<string>("ZplData");
        
        var pdf = await _converter.Convert(zpl);
        
        var leg = new Leg
        {
            LegId = Guid.NewGuid(),
            LegData = context.Arguments.LegData,
            ZplData = context.Arguments.ZplData,
            PdfData = pdf.Data
        };
        
        return context.CompletedWithVariables<ConvertLabelLog>(new {leg}, new {leg});
    }

    public Task<CompensationResult> Compensate(CompensateContext<ConvertLabelLog> context)
    {
        throw new NotImplementedException();
    }
}