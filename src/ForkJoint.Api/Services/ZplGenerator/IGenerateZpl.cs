using System.Threading.Tasks;
using ForkJoint.Api.Components.Activities;

namespace ForkJoint.Api.Services.ZplGenerator;

public interface IGenerateZpl
{
    Task<ZplLabel> Generate(string data);
    void Add(ZplLabel zpl);
}
