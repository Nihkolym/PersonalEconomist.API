using PersonalEconomist.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PersonalEconomist.Services.Services.CreditCardService
{
    public class CreditCardService : ICreditCardService
    {
        private readonly PersonalEconomistDbContext _context;

        public CreditCardService(PersonalEconomistDbContext context)
        {
            _context = context;
        }

        public async Task<double> Replenish(Guid cardId, double amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException();
            }

            var card = _context.CreditCards.FirstOrDefault(c => c.Id == cardId);

            card.Amount += amount;

            _context.Update(card);

            await _context.SaveChangesAsync();

            return _context.CreditCards.FirstOrDefault(c => c.Id == cardId).Amount;
        }

        public async Task<double> Withdraw(Guid cardId, double amount)
        {
            var currentCardAmount = _context.CreditCards.FirstOrDefault(c => c.Id == cardId).Amount;

            if (amount < 0 || amount > currentCardAmount)
            {
                throw new InvalidOperationException();
            }

            var card = _context.CreditCards.FirstOrDefault(c => c.Id == cardId);

            card.Amount -= amount;

            _context.Update(card);

            await _context.SaveChangesAsync();

            return _context.CreditCards.FirstOrDefault(c => c.Id == cardId).Amount;
        }
    }
}
