using PersonalEconomist.Entities.Models.Indication;
using PersonalEconomist.Entities.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.IndicationStore
{
    public interface IIndicationStore
    {
        Task<IndicationDTO> AddIndication(IndicationDTO modelDto);
        Task<IndicationDTO> GetIndication(Guid Id);
        Task<IEnumerable<IndicationDTO>> GetAll();
    }
}
