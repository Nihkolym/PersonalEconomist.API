using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.Auth;
using PersonalEconomist.Entities.Models.User;

namespace PersonalEconomist.Services.Services.AuthService
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterDTO model);
        Task<LoginResponseDTO> Login(UserLoginDTO model);
    }
}
