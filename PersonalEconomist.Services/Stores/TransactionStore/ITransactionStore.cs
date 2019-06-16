using PersonalEconomist.Entities.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.TransactionStore
{
    public interface ITransactionStore
    {
        Task<TransactionDTO> AddTransaction(TransactionDTO modelDto);
        Task<TransactionDTO> GetTransaction(Guid Id);
        Task<IEnumerable<TransactionDTO>> AllTransactions(Guid cardId);
        Task<IEnumerable<TransactionDTO>> GetUserTransactions(string userId);
    }
}
