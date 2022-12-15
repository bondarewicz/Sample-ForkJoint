using ForkJoint.Domain.ShipmentLine;

namespace ForkJoint.Domain.Leg;

public interface LegCompleted : ShipmentLineCompleted
{
    Leg Leg { get; }
}