namespace ForkJoint.Domain.Leg;

public interface RequestLabelGeneration : ShipmentLine.ShipmentLine
{
    Leg Leg { get; }
}