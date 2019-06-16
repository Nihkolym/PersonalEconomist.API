using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Activity
{
    public class ActivityDTO : BaseGuidDTOEntity
    {
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        public ICollection<Item.ItemDTO> Items { set; get; } = new List<Item.ItemDTO>();
    }
}
