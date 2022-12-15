using System;
using System.ComponentModel.DataAnnotations;
using ForkJoint.Domain.Invoice;
using ForkJoint.Domain.Label;

namespace ForkJoint.Api.Models;

public class Shipment
{
    [Required]
    public Guid ShipmentId { get; init; }
    public Label[] Labels { get; init; }
    public Invoice[] Invoices { get; init; }
}