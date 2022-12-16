namespace ForkJoint.Domain.Leg;

public interface CreateLegLabel : ShipmentLine.ShipmentLine
{
    Leg Leg { get; }
}