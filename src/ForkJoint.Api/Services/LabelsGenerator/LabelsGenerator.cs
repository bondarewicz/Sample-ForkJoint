using System.Collections.Generic;
using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Services.ZplGenerator;

public class LabelsGenerator : IGenerateLabels
{
    readonly ILogger<LabelsGenerator> _logger;
    
    // todo keep in mem or for compensate
    readonly HashSet<ZplLabel> _zpls;
    
    public LabelsGenerator(ILogger<LabelsGenerator> logger)
    {
        _logger = logger;
        _zpls = new HashSet<ZplLabel>();
    }
    
    public async Task<ZplLabel> Generate(string data)
    {
        _logger.LogDebug("Generating ZPL for {data}", data);
        
        await Task.Delay(5000);

        var zpl = Base64Encode(data);
        return new ZplLabel
        {
            Data = $"{zpl}"
        };
    }

    public void Add(ZplLabel zpl)
    {
        throw new System.NotImplementedException();
    }

    public static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}