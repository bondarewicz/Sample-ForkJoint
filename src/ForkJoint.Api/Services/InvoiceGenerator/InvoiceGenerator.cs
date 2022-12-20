using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Services.InvoiceGenerator;

public class InvoiceGenerator : IGenerateInvoice
{
    readonly ILogger<InvoiceGenerator> _logger;

    public InvoiceGenerator(ILogger<InvoiceGenerator> logger)
    {
        _logger = logger;
    }
    
    public async Task<ZplInvoice> Generate(int quantity)
    {
        _logger.LogDebug($"Generating Invoice x {quantity}");
        
        await Task.Delay(2000);

        var zpl = Base64Encode(Guid.NewGuid().ToString());
        return new ZplInvoice
        {
            Data = $"{zpl}"
        };
    }
    
    public static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}