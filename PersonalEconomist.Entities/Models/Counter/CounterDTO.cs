using PersonalEconomist.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalEconomist.Entities.Models.Counter
{
    public class CounterDTO : BaseGuidDTOEntity
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public ICollection<Indication.IndicationDTO> Indications { set; get; } = new List<Indication.IndicationDTO>();
    }
}
