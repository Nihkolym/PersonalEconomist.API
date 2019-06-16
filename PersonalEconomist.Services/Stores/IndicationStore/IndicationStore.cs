using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersonalEconomist.Domain;
using PersonalEconomist.Domain.Models.Indication;
using PersonalEconomist.Entities.Models.Indication;
using PersonalEconomist.Entities.Models.Item;

namespace PersonalEconomist.Services.Stores.IndicationStore
{
    public class IndicationStore : IIndicationStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public IndicationStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IndicationDTO> AddIndication(IndicationDTO modelDto)
        {
            var model = _mapper.Map<Indication>(modelDto);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<IndicationDTO>(model);
        }

        public async Task<IEnumerable<IndicationDTO>> GetAll()
        {
            var models = _context.Indications.ToList();

            return _mapper.Map<IEnumerable<IndicationDTO>>(models);
        }

        public async Task<IndicationDTO> GetIndication(Guid Id)
        {
            return _mapper.Map<IndicationDTO>(_context.Indications.FirstOrDefault(i => i.Id == Id));
        }
    }
}
