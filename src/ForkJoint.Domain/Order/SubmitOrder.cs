using System;

namespace ForkJoint.Domain.Order;

public interface SubmitOrder
{
    Guid OrderId { get; }

    Burger.Burger[] Burgers { get; }
    Fry.Fry[] Fries { get; }
    Shake.Shake[] Shakes { get; }
    FryShake.FryShake[] FryShakes { get; }
}