using System.Threading.Tasks;

namespace ForkJoint.Api.Services.ReceiptGenerator;

public interface IGenerateReceipt
{
    Task<ZplReceipt> Generate(int quantity);
}

public record ZplReceipt
{
    public string Data { get; init; }
}