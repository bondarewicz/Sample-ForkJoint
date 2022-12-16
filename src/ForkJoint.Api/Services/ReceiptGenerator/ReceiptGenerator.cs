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

    public async Task<ZplReceipt> Generate(string data)
    {
        _logger.LogDebug("Generating Receipt for {data}", data);
        
        await Task.Delay(2000);

        var zpl = RandomString(5);
        return new ZplReceipt
        {
            Data = $"{data}-{zpl}"
        };
    }
    
    private static string RandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}