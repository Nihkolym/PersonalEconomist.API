using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersonalEconomist.Domain;
using Microsoft.EntityFrameworkCore;
using PersonalEconomist.Domain.Models.Transaction;
using PersonalEconomist.Domain.Models.TransactionItem;
using PersonalEconomist.Entities.Models.Transaction;
using System.Linq;
using PersonalEconomist.Services.Services.CreditCardService;
using PersonalEconomist.Services.Stores.UserStore;
using PersonalEconomist.Services.Stores.CreditCardStore;

namespace PersonalEconomist.Services.Stores.TransactionStore
{
    public class TransactionStore : ITransactionStore
    {
        private readonly IMapper _mapper;
        private readonly ICreditCardService _creditCardService;
        private readonly ICreditCardStore _creditCardStore;
        private readonly PersonalEconomistDbContext _context;

        public TransactionStore(
            IMapper mapper, 
            ICreditCardService creditCardService,
            ICreditCardStore creditCardStore,
            PersonalEconomistDbContext context,
            IUserStore userStore
        )
        {
            _mapper = mapper;
            _creditCardService = creditCardService;
            _creditCardStore = creditCardStore;
            _context = context;
        }

        public async Task<TransactionDTO> AddTransaction(TransactionDTO modelDto)
        {
            var transactions = new List<TransactionItem>();

            modelDto.Amount = 0;

            var allItems = _context.Items.ToList();

            var items = modelDto.Items.Select(i => allItems.Find(ci => ci.Id == i.Id));

            foreach (var item in items)
            {
                modelDto.Amount += item.Price;
            }

            var model = _mapper.Map<Transaction>(modelDto);

            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _creditCardService.Withdraw(model.CreditCardId, modelDto.Amount.Value);

                    await _context.AddAsync(model);
                    await _context.SaveChangesAsync();

                    foreach (var item in modelDto.Items)
                    {
                        transactions.Add(new TransactionItem()
                        {
                            TransactionId = model.Id,
                            ItemId = item.Id,
                        });
                    }

                    _context.AddRange(transactions);
                    await _context.SaveChangesAsync();

                    var user = _context.CreditCards.Include(c => c.User).FirstOrDefault(c => c.Id == modelDto.CreditCardId).User;

                    user.Amount += modelDto.Amount.Value;

                    _context.Users.Update(user);

                    await _context.SaveChangesAsync();

                    _transaction.Commit();
                } 
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                
            }

            var transaction = _context.Transactions.Include(t => t.TransactionItems).ThenInclude(ti => ti.Item).FirstOrDefault(t => t.Id == model.Id);

            return _mapper.Map<TransactionDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetUserTransactions(string userId)
        {
            var creditCards = await _creditCardStore.getCards(userId);

            var models = _context.Transactions.Include(t => t.TransactionItems).ThenInclude(ti => ti.Item).Include(t => t.CreditCard).ToArray().Where(t => creditCards.Any(c => c.Id == t.CreditCardId));

            return _mapper.Map<IEnumerable<TransactionDTO>>(models);
        }

        public async Task<IEnumerable<TransactionDTO>> AllTransactions(Guid cardId)
        {
            var models = _context.Transactions.Include(t => t.TransactionItems).ThenInclude(ti => ti.Item).Where(t => t.CreditCardId == cardId);

            return _mapper.Map<IEnumerable<TransactionDTO>>(models);
        }

        public async Task<TransactionDTO> GetTransaction(Guid Id)
        {
            return _mapper.Map<TransactionDTO>(_context.Transactions.Include(t => t.TransactionItems).ThenInclude(ti => ti.Item).FirstOrDefault(i => i.Id == Id));
        }
    }
}
