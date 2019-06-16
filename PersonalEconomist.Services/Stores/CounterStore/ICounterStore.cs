using PersonalEconomist.Entities.Models.Counter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.CounterStore
{
    public interface ICounterStore
    {
        Task<IEnumerable<CounterDTO>> GetCounters(string Id);
        Task<CounterDTO> AddCounter(CounterDTO counter);
        Task<CounterDTO> GetCounter(Guid Id);
    }
}
