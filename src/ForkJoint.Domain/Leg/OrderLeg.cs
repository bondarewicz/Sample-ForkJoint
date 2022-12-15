namespace ForkJoint.Domain.Leg;

public interface OrderLeg : ShipmentLine.ShipmentLine
{
    Leg Leg { get; }
}