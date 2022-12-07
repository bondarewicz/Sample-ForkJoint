namespace ForkJoint.Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using Domain.Burger;
using Domain.Fry;
using Domain.FryShake;
using Domain.Shake;


public class Order
{
    [Required]
    public Guid OrderId { get; init; }

    public Burger[] Burgers { get; init; }
    public Fry[] Fries { get; init; }
    public Shake[] Shakes { get; init; }
    public FryShake[] FryShakes { get; init; }
}