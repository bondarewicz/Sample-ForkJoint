using System;

namespace ForkJoint.Contracts.Shake;

public interface ShakeReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }

    string Flavor { get; }
    Size Size { get; }
}