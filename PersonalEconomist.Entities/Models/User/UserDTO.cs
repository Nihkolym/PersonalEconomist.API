using PersonalEconomist.Entities.Models.Base;
using PersonalEconomist.Entities.Models.Goal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Entities.Models.User
{
    public class UserDTO : BaseGuidDTOEntity
    {
        public string UserName { get; set; }
        public double Amount { get; set; }
        public string Avatar { get; set; }
        public virtual List<Goal.GoalDTO> Goals { get; set; }
        public virtual List<CreditCard.CreditCardDTO> CreditCards { get; set; }
        public virtual List<Counter.CounterDTO> Counters { get; set; }
    }
}
