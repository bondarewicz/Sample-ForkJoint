using System;

namespace ForkJoint.Domain.OnionRings;

public interface CookOnionRings
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }

    int Quantity { get; }
}