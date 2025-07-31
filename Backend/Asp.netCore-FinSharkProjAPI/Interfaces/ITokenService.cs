using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
