using System;

namespace ForkJoint.Domain.Label;

public record Label
{
    public Guid LabelId { get; init; }
    public string Data { get; init; }
}