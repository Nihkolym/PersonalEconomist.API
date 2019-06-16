using PersonalEconomist.Entities.Models.Base;
using PersonalEconomist.Entities.Models.Item;
using PersonalEconomist.Entities.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Entities.Models.TransactionItem 
{
    public class TransactionItemDTO : BaseGuidDTOEntity
    {
        public Guid? TransactionId { get; set; }
        public Guid? ItemId { get; set; }

        public virtual TransactionDTO TransactionDTO { get; set; }
        public virtual ItemDTO ItemDTO { get; set; }
    }
}
