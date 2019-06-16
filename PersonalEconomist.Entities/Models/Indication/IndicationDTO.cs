using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalEconomist.Entities.Models.Indication
{
    public class IndicationDTO: BaseGuidDTOEntity
    {
        public DateTime Date { set; get; }
        public int Value { set; get; }
        [Required]
        public Guid CounterId { get; set; }
    }
}
