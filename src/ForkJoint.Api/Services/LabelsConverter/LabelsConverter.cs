using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Services.LabelsConverter;

public class LabelsConverter : IConvertLabels
{
    readonly ILogger<LabelsConverter> _logger;

    public LabelsConverter(ILogger<LabelsConverter> logger)
    {
        _logger = logger;
    }

    public async Task<PdfLabel> Convert(string data)
    {
        _logger.LogDebug($"Converting ZPL {data} to PDF");
        
        await Task.Delay(5000);

        var pdf = string.Concat(data, ".pdf");
        
        return new PdfLabel
        {
            Data = $"{pdf}"
        };
    }
}