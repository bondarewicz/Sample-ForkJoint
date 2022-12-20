using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;

namespace ForkJoint.Api.Services.ZplGenerator;

public interface IGenerateLabels
{
    Task<ZplLabel> Generate(string data);
    void Add(ZplLabel zpl);
}
