using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Asp.netCore_FinSharkProjAPI.Services
{
    public class ServiceToken : ITokenService
    {
        private readonly IConfiguration config;
        private readonly SymmetricSecurityKey key;

        public ServiceToken(IConfiguration config)
        {
            this.config = config;
            // JWT:SigningKey config file (appsettings.json) se ek secret key le raha hai.
            key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:SigningKey"]));
        }
        public string CreateToken(AppUser user)
        {
            /* JWT ke andar kuch data pack karte hain. Isse kehte hain Claims.
               Tum JWT ke andar Email aur UserName bhej rahe ho. */
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            /* JWT ko sign karne ke liye key aur HmacSha256Signature algorithm use kiya gaya.
               Signing ka matlab: koi token fake na bana sake. */
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), 
                SigningCredentials = creds,
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"]

            };

            // JWT token create karne ke liye JwtSecurityTokenHandler ka use kiya gaya. TokenHandler JWT generate karta hai.
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); //CreateToken() — JWT object banata hai.
            return tokenHandler.WriteToken(token);  //WriteToken() — us object ko string mein convert karta hai (jo frontend ko bhejoge).

        }
    
    }
}
