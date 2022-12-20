using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;

namespace ForkJoint.Api.Services.LabelsConverter;

public interface IConvertLabels
{
    Task<PdfLabel> Convert(string data);
}