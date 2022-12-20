using System;
using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Leg;

public interface LegLabelCompleted : ShipmentLineCompleted
{
    Leg Leg { get; }
}