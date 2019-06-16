using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PersonalEconomist.Entities.Models.User;
using PersonalEconomist.WebAPI;
using PersonalEconomist.Services.Helpers;
using System.Security.Principal;
using PersonalEconomist.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using PersonalEconomist.Domain.Models.User;

namespace PersonalEconomist.Services.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private UserManager<User> _userManager;

        public TokenService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateEncodedToken(string email, string userName, ClaimsIdentity identity)
        {

            var now = DateTime.UtcNow;

            User user = null;

            if (!string.IsNullOrEmpty(email))
            {
                user = await _userManager.FindByEmailAsync(email);
            }

            if (user == null && !string.IsNullOrEmpty(userName))
            {
                user = await _userManager.FindByNameAsync(userName);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await JWTOptions.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, UnixEpochDateGenerator.ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                            identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Rol),
                            identity.FindFirst(Constants.Strings.JwtClaimIdentifiers.Id)
            };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: JWTOptions.ISSUER,
                    audience: JWTOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(JWTOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(JWTOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        public ClaimsIdentity GenerateClaimsIdentity(string email, string id, string rol)
        {
            return new ClaimsIdentity(new GenericIdentity(email, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, rol)
            });
        }

    }
}
