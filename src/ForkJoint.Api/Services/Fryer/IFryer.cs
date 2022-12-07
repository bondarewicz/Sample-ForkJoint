namespace ForkJoint.Api.Services.Fryer;

using System.Threading.Tasks;
using Contracts;

public interface IFryer
{
    Task CookOnionRings(int quantity);

    Task CookFry(Size size);
}