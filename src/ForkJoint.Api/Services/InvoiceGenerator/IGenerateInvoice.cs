using System.Threading.Tasks;

namespace ForkJoint.Api.Services.InvoiceGenerator;

public interface IGenerateInvoice
{
    Task<ZplInvoice> Generate(int quantity);
}


public record ZplInvoice
{
    public string Data { get; init; }
}