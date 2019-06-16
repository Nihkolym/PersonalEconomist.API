using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Entities.Models.Auth
{
    public class LoginResponseDTO
    {
        public string token;
        public bool isAdmin;
        public string userName;
    }
}
