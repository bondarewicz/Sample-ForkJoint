namespace ForkJoint.Api.Services.Fryer;

using System.Threading.Tasks;
using Domain;

public class Fryer :
    IFryer
{
    public async Task CookOnionRings(int quantity)
    {
        await Task.Delay(1000);
    }

    public async Task CookFry(Size size)
    {
        await Task.Delay(1000);
    }
}