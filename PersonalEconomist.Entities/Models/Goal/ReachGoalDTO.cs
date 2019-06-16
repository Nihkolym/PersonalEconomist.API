using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Goal
{
    public class ReachGoal
    {

        [Required]
        public Guid GoalId { get; set; }
        [Required]
        public Guid CreditCardId { get; set; }
    }
}
