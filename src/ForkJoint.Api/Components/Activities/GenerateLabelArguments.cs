using System;
using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Components.Activities;

public interface GenerateLabelArguments
{
    Guid ShipmentId { get; }
    string LegData { get; }
    string ZplData { get; }
    ZplLabel ZplLabel { get; }
}