using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.User;
using PersonalEconomist.Services.Stores.UserStore;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private IUserStore _userStore;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserController(IUserStore userStore, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userStore = userStore;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            var user = await _userStore.GetUser(userId);
            return Ok(_mapper.Map<UserDTO>(user));
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UserDTO model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;

            var res = await _userStore.UpdateUser(userId, model);

            if (res != null)
            {
                return Ok(res);
            } 
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
