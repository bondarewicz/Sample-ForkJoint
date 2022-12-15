using System;
using System.Collections.Generic;

namespace ForkJoint.Domain.Order;

public interface SubmitOrder
{
    Guid OrderId { get; }

    IEnumerable<Burger.Burger> Burgers { get; }
    IEnumerable<Fry.Fry> Fries { get; }
    IEnumerable<Shake.Shake> Shakes { get; }
    IEnumerable<FryShake.FryShake> FryShakes { get; }
}