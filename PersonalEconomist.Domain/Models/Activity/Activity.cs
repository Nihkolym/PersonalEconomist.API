using PersonalEconomist.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalEconomist.Domain.Models.Activity
{
    public class Activity : BaseGuidEntity
    {
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Item.Item> Items { set; get; } = new List<Item.Item>();
    }
}
