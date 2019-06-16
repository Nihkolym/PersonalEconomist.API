using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PersonalEconomist.Domain.Models.Base;

namespace PersonalEconomist.Domain.Models.CreditCard
{
    public class CreditCard : BaseGuidEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User.User User { get; set; }
        [Required]
        public double Amount { get; set; } = 0;
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string CardNumber { get; set; }
        public ICollection<Transaction.Transaction> Transactions { get; set; } = new List<Transaction.Transaction>();
    }
}
