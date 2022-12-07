namespace ForkJoint.Api.Services.ShakeMachine;

using System.Threading.Tasks;
using Contracts;

public interface IShakeMachine
{
    Task PourShake(string flavor, Size size);
}