using System;

namespace ForkJoint.Domain.OnionRings;

public interface OnionRingsReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    int Quantity { get; }
}