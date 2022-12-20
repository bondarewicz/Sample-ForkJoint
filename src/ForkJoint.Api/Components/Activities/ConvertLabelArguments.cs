using System;

namespace ForkJoint.Api.Components.Activities;

public interface ConvertLabelArguments
{
    Guid ShipmentId { get; }
    string LegData { get; }
    string ZplData { get; }
}