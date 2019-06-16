using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.WebAPI
{
    public class JWTOptions
    {
        public const string ISSUER = "PersonalEconomistAuthServer"; 
        public const string AUDIENCE = "http://localhost:5001/"; 
        const string KEY = "mysupersecret_secretkey!123"; 
        public const int LIFETIME = 1440; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static Func<Task<string>> NonceGenerator { get; set; }
            = new Func<Task<string>>(() => Task.FromResult(Guid.NewGuid().ToString()));
    }
}
