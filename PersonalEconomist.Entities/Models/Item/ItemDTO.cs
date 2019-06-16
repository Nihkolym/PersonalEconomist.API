using Microsoft.AspNetCore.Http;
using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Item
{
    public class ItemDTO : BaseGuidDTOEntity
    {
        [Required]
        public string Title { get; set; }
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ActivityId { get; set; }
    }
}
