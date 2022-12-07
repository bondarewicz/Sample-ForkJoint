namespace ForkJoint.Api.Services.ShakeMachine;

using System.Threading.Tasks;
using Domain;

public class ShakeMachine :
    IShakeMachine
{
    public async Task PourShake(string flavor, Size size)
    {
        await Task.Delay(1000);
    }
}