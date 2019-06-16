using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalEconomist.Domain.Models.Item
{
    public class Item : BaseGuidEntity
    {
        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }
        public Activity.Activity Activity { get; set; }
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        public virtual ICollection<TransactionItem.TransactionItem> TransactionItems { get; set; } = new HashSet<TransactionItem.TransactionItem>();
    }
}
