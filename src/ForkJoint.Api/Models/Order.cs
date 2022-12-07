using ForkJoint.Contracts.Burger;
using ForkJoint.Contracts.Fry;
using ForkJoint.Contracts.FryShake;
using ForkJoint.Contracts.Shake;

namespace ForkJoint.Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using Contracts;


public class Order
{
    [Required]
    public Guid OrderId { get; init; }

    public Burger[] Burgers { get; init; }
    public Fry[] Fries { get; init; }
    public Shake[] Shakes { get; init; }
    public FryShake[] FryShakes { get; init; }
}