namespace ForkJoint.Api.Services.ShakeMachine;

using System.Threading.Tasks;
using Domain;

public interface IShakeMachine
{
    Task PourShake(string flavor, Size size);
}