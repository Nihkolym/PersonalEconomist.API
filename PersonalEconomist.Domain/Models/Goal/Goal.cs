using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalEconomist.Domain.Models.Goal
{
    public class Goal : BaseGuidEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User.User User{ get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Amount { get; set; }

        public string Image { get; set; }
        public bool IsMain { get; set; }
    }
}
