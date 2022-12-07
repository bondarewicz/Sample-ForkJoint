using System;

namespace ForkJoint.Contracts.OnionRings;

public interface CookOnionRings
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }

    int Quantity { get; }
}