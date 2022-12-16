using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Services.ZplGenerator;

public class LabelsGenerator : IGenerateLabels
{
    readonly ILogger<LabelsGenerator> _logger;
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

        var zpl = RandomString(5);
        return new ZplLabel
        {
            Data = $"{data}-{zpl}"
        };
    }

    public void Add(ZplLabel zpl)
    {
        throw new System.NotImplementedException();
    }

    private static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}