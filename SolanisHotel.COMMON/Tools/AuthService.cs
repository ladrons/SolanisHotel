using SolanisHotel.ENTITIES.Models.Common;
using System.Security.Claims;


namespace SolanisHotel.COMMON.Tools
{
    public static class AuthService
    {
       public static ClaimsPrincipal CreatePrincipal(BaseUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "LoginUser");

            return new ClaimsPrincipal(userIdentity);
        }
    }
}