using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalEconomist.Domain.Models.Transaction
{
    public class Transaction : BaseGuidEntity
    {
        [ForeignKey("CreditCard")]
        [Required]
        public Guid CreditCardId { get; set; }
        public virtual CreditCard.CreditCard CreditCard{ get; set; }
        [Required] 
        public double Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual ICollection<TransactionItem.TransactionItem> TransactionItems { get; set; } = new HashSet<TransactionItem.TransactionItem>();
    }
}
