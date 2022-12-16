namespace ForkJoint.Api.Components.Activities;

public interface GenerateReceiptLog
{
    ZplReceipt ZplReceipt { get; }
}

public record ZplReceipt
{
    public string Data { get; init; }
}