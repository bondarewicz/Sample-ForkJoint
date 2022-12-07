using System;

namespace ForkJoint.Contracts.Fry;

public interface FryReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    Size Size { get; }
}