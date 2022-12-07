namespace ForkJoint.Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Burger;
using Contracts.Fry;
using Contracts.FryShake;
using Contracts.Shake;


public class Order
{
    [Required]
    public Guid OrderId { get; init; }

    public Burger[] Burgers { get; init; }
    public Fry[] Fries { get; init; }
    public Shake[] Shakes { get; init; }
    public FryShake[] FryShakes { get; init; }
}