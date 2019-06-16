using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.Transaction;
using PersonalEconomist.Services.Services.CreditCardService;
using PersonalEconomist.Services.Stores.TransactionStore;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class TransactionController : Controller
    {
        private readonly ITransactionStore _transactionStore;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(ITransactionStore transactionStore, IHttpContextAccessor httpContextAccessor)
        {
            _transactionStore = transactionStore;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(Guid cardId)
        {
            return Ok(await _transactionStore.AllTransactions(cardId));
        }

        [HttpGet("user")]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;

            return Ok(await _transactionStore.GetUserTransactions(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _transactionStore.GetTransaction(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TransactionDTO value)
        {
            return Ok(await _transactionStore.AddTransaction(value));
        }
    }
}
