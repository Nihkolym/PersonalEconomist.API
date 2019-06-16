using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalEconomist.Domain.Models.TransactionItem
{
    public class TransactionItem : BaseGuidEntity
    {
        [ForeignKey("Transaction")]
        public Guid? TransactionId { get; set; }
        [ForeignKey("Item")]
        public Guid? ItemId { get; set; }

        public Transaction.Transaction Transaction { get; set; }
        public Item.Item Item { get; set; }
    }
}
