using System;

namespace ForkJoint.Domain.FryShake;

public interface FryShakeReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    string Flavor { get; }
    Size Size { get; }
}