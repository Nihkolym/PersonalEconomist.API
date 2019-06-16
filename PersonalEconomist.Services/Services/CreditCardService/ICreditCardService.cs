using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Services.CreditCardService
{
    public interface ICreditCardService
    {
        Task<double> Replenish(Guid cardId, double amount);
        Task<double> Withdraw(Guid cardId, double amount);
    }
}
