using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersonalEconomist.Domain;
using PersonalEconomist.Domain.Models.CreditCard;
using PersonalEconomist.Entities.Models.CreditCard;
using Microsoft.EntityFrameworkCore;

namespace PersonalEconomist.Services.Stores.CreditCardStore
{
    public class CreditCardStore : ICreditCardStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public CreditCardStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreditCardDTO> addCard(CreditCardDTO card, string userId)
        {
            var model = _mapper.Map<CreditCard>(card);

            model.UserId = userId;

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<CreditCardDTO>(model);
        }

        public async Task<CreditCardDTO> getCard(Guid Id)
        {
            return _mapper.Map<CreditCardDTO>(_context.CreditCards.Include(c => c.Transactions).FirstOrDefault(c => c.Id == Id));
        }

        public async Task<List<CreditCardDTO>> getCards(string userId)
        {
            return _mapper.Map<List<CreditCardDTO>>(_context.CreditCards.Include(c => c.Transactions).Where(c => c.UserId == userId));
        }
    }
}
