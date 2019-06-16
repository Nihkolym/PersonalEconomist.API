using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.User;
using PersonalEconomist.Services.Services.TokenService;
using PersonalEconomist.Domain.Identity;
using PersonalEconomist.Domain;
using System.Linq;
using PersonalEconomist.Entities.Models.Auth;

namespace PersonalEconomist.Services.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly PersonalEconomistDbContext _context;


        public AuthService(
            IMapper mapper, 
            UserManager<User> userManager, 
            PersonalEconomistDbContext context,
            SignInManager<User> signInManager,
            ITokenService tokenService
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }


        public async Task<bool> Register(UserRegisterDTO model)
        {
            try
            {
                User user = new User
                {
                    UserName = model.UserName,
                    Amount = 0,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User);

                    await _signInManager.SignInAsync(user, false);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }

        }

        public async Task<LoginResponseDTO> Login(UserLoginDTO model)
        {
            var identity = await GetClaimsIdentity(model.Email, model.UserName, model.Password);

            if (identity == null)
            {
                return null;
            }

            var token = await _tokenService.GenerateEncodedToken(model.Email, model.UserName, identity);

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email || u.UserName == model.UserName);

            var isAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin);


            return new LoginResponseDTO() { isAdmin = isAdmin, token = token, userName = user.UserName };
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string username, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                User userToVerify = null;

                if (!string.IsNullOrEmpty(username)) {
                    userToVerify = await _userManager.FindByNameAsync(username);
                }


                if (userToVerify == null && !string.IsNullOrEmpty(email))
                {
                    userToVerify = await _userManager.FindByEmailAsync(email);
                }

                if (userToVerify != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(userToVerify);
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_tokenService.GenerateClaimsIdentity(userToVerify.UserName, userToVerify.Id, userRoles[0]));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
