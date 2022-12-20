using System;
using System.Text;

namespace ForkJoint.Domain.Leg;

public record Leg
{
    public Guid LegId { get; init; }
    public string LegData { get; init; }
    public string LabelData { get; init; }
    public bool Labels { get; init; }
    public bool Invoice { get; init; }
    public bool Receipt { get; init; }
    
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendFormat("Label: {0}", LabelData);

        if (Labels)
            sb.Append(" Labels");
        if (Invoice)
            sb.Append(" Invoice");
        if (Receipt)
            sb.Append(" Receipt");

        return sb.ToString();
    }
}