using System;

namespace ForkJoint.Contracts.Order;

public interface SubmitOrder
{
    Guid OrderId { get; }

    Burger.Burger[] Burgers { get; }
    Fry.Fry[] Fries { get; }
    Shake.Shake[] Shakes { get; }
    FryShake.FryShake[] FryShakes { get; }
}