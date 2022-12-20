using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ForkJoint.Api.Services.ReceiptGenerator;

public class ReceiptGenerator : IGenerateReceipt
{
    readonly ILogger<ReceiptGenerator> _logger;

    public ReceiptGenerator(ILogger<ReceiptGenerator> logger)
    {
        _logger = logger;
    }

    public async Task<ZplReceipt> Generate(int quantity)
    {
        _logger.LogDebug($"Generating Receipt x {quantity}");
        
        await Task.Delay(2000);

        var zpl = Base64Encode(Guid.NewGuid().ToString());
        return new ZplReceipt
        {
            Data = $"{zpl}"
        };
    }
    
    public static string Base64Encode(string plainText) {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}