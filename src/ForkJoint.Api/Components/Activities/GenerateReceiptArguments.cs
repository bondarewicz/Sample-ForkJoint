using System;

namespace ForkJoint.Api.Components.Activities;

public interface GenerateReceiptArguments
{
    Guid ShipmentId { get; }
    // string LegData { get; }
    string Data { get; }
    public bool Labels { get; }
    public bool Invoice { get; }
    public bool Receipt { get; }
}