using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.User;
using PersonalEconomist.Services.Services.AuthService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")] 
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/<controller>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO model)
        {
            var result = await _authService.Register(model);

            if (result)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        // POST api/<controller>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDTO model)
        {
            var res = await _authService.Login(model);

            if (res == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }
    }
}
