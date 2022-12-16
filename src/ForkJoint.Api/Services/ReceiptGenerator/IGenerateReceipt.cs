using System.Threading.Tasks;

namespace ForkJoint.Api.Services.ReceiptGenerator;

public interface IGenerateReceipt
{
    Task<ZplReceipt> Generate(string data);
}

public record ZplReceipt
{
    public string Data { get; init; }
}