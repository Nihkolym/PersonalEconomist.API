using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Domain.Models.Counter
{
    public class Counter : BaseGuidEntity
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Indication.Indication> Indications { set; get; } = new List<Indication.Indication>();
    }
}
