using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PersonalEconomist.Domain.Models.User
{
    public class User : IdentityUser
    {
        [MaxLength(36)]
        public override string Id { get; set; }
        [MaxLength(36)]
        public override string UserName { get; set; }
        [MaxLength(36)]
        public double Amount { get; set; }
        public string Avatar { get; set; }
        public ICollection<Goal.Goal> Goals { get; set; } = new List<Goal.Goal>();
        public ICollection<CreditCard.CreditCard> CreditCards { get; set; } = new List<CreditCard.CreditCard>();
        public ICollection<Counter.Counter> Counters { get; set; } = new List<Counter.Counter>();
    }
}
