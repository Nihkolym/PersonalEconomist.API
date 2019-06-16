using PersonalEconomist.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalEconomist.Domain.Models.Indication
{
    public class Indication : BaseGuidEntity
    {
        [ForeignKey("Counter")]
        public Guid CounterId { get; set; }
        public Counter.Counter Counter { get; set; }
        public DateTime Date { set; get; }
        public int Value { set; get; }
    }
}
