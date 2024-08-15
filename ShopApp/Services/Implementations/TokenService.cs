using Microsoft.IdentityModel.Tokens;
using ShopApp.Entities;
using ShopApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopApp.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string GetToken(string secretKey, string audience, string issuer, AppUser user, IList<string> roles)
        {
            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(secretKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var claim = new ClaimsIdentity();

            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claim.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claim.AddClaim(new Claim(ClaimTypes.GivenName, user.FullName));
            claim.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claim.AddClaim(new Claim("Location", "Baku/Bilajari"));

            claim.AddClaims(roles.Select(l => new Claim(ClaimTypes.Role, l)).ToList());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(15),
                Subject = claim,
                Audience = audience,
                Issuer = issuer
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
