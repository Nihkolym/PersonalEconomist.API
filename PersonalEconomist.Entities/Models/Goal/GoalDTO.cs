using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PersonalEconomist.Entities.Models.Goal
{
    public class GoalDTO : BaseGuidDTOEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Amount { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
        public bool? IsMain { get; set; }
    }
}
