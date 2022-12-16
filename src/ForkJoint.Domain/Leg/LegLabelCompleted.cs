using System;
using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Leg;

public interface LegLabelCompleted : ShipmentLineCompleted
{
    Guid ShipmentId { get; }
    Leg Leg { get; }
}