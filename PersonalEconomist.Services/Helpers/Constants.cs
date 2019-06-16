using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Services.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class Admin
            {
                public const string Email = "565189@gmail.com";
                public const string Password = "123456";
            }
        }
    }

}
