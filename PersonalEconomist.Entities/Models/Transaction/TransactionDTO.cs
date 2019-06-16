using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Transaction
{
    public class TransactionDTO : BaseGuidDTOEntity
    {
        [Required]
        public Guid? CreditCardId { get; set; }
        [Required]
        public double? Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        public CreditCard.CreditCardDTO CreditCard { get; set; }

        public virtual List<Item.ItemDTO> Items { get; set; }
    }
}
