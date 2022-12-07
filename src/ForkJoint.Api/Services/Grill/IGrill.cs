using System.Threading.Tasks;
using ForkJoint.Contracts.Burger;

namespace ForkJoint.Api.Services.Grill;

public interface IGrill
{
    Task<BurgerPatty> CookOrUseExistingPatty(decimal weight, bool cheese);
    void Add(BurgerPatty patty);
}