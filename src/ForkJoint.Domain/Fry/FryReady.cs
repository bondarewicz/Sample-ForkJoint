using System;

namespace ForkJoint.Domain.Fry;

public interface FryReady
{
    Guid OrderId { get; }
    Guid OrderLineId { get; }
    Size Size { get; }
}