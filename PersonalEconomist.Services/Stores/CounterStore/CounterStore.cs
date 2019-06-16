using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalEconomist.Domain;
using PersonalEconomist.Entities.Models.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.CounterStore
{
    public class CounterStore : ICounterStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public CounterStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CounterDTO> AddCounter(CounterDTO counter)
        {
            var model = _mapper.Map<Domain.Models.Counter.Counter>(counter);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<CounterDTO>(model);
        }

        public async Task<CounterDTO> GetCounter(Guid Id)
        {
            return _mapper.Map<CounterDTO>(_context.Counters.FirstOrDefault(c => c.Id == Id));
        }

        public async Task<IEnumerable<CounterDTO>> GetCounters(string Id)
        {
            return _mapper.Map<List<CounterDTO>>(_context.Counters.Include(counter => counter.Indications).Where(c => c.UserId == Id).AsEnumerable());
        }
    }
}
