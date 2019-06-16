using PersonalEconomist.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Services.TokenService
{
    public interface ITokenService
    {
        Task<string> GenerateEncodedToken(string email, string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, string role);
    }
}
