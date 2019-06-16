using PersonalEconomist.Entities.Models.CreditCard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.CreditCardStore
{
    public interface ICreditCardStore
    {
        Task<CreditCardDTO> addCard(CreditCardDTO model, string userId);
        Task<List<CreditCardDTO>> getCards(string userId);
        Task<CreditCardDTO> getCard(Guid cardId);
    }
}
