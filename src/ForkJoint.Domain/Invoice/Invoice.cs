using System;

namespace ForkJoint.Domain.Invoice;

public record Invoice
{
    public Guid InvoiceId { get; init; }
    public string Data { get; init; }
}