using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.CreditCard;
using PersonalEconomist.Services.Services.CreditCardService;
using PersonalEconomist.Services.Stores.CreditCardStore;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardStore _creditCardStore;
        private readonly ICreditCardService _creditCardService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreditCardController(ICreditCardStore creditCardStore, ICreditCardService creditCardService, IHttpContextAccessor httpContextAccessor)
        {
            _creditCardStore = creditCardStore;
            _creditCardService = creditCardService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            return Ok(await _creditCardStore.getCards(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _creditCardStore.getCard(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreditCardDTO value)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;
            return Ok(await _creditCardStore.addCard(value, userId));
        }

        [HttpPost("replenish")]
        public async Task<IActionResult> Post(Guid cardId, double amount)
        {
            return Ok(await _creditCardService.Replenish(cardId, amount));
        }
    }
}
