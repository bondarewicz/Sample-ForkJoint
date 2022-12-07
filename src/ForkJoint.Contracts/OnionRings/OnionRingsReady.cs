using System;

namespace ForkJoint.Contracts.OnionRings;

public interface OnionRingsReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    int Quantity { get; }
}