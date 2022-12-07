using System;

namespace ForkJoint.Domain.Fry;

public record Fry
{
    public Guid FryId { get; init; }
    public Size Size { get; init; }

    public override string ToString()
    {
        return $"{Size} Fry";
    }
}