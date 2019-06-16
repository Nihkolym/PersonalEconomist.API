using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Services.Stores.CounterStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CounterController : Controller
    { 
        private readonly ICounterStore _counterStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CounterController(ICounterStore counterStore, IHttpContextAccessor httpContextAccessor)
        {
            _counterStore = counterStore;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;

            return Ok(await _counterStore.GetCounters(userId));
        }
    }
}
