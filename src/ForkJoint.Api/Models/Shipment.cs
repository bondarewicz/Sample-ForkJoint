using System;
using System.ComponentModel.DataAnnotations;
using ForkJoint.Domain.Leg;

namespace ForkJoint.Api.Models;

public class Shipment
{
    [Required]
    public Guid ShipmentId { get; init; }
    public Leg[] Legs { get; init; }
}