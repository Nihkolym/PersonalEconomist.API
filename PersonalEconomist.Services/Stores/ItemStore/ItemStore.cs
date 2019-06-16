using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalEconomist.Domain;
using PersonalEconomist.Domain.Models.Goal;
using PersonalEconomist.Domain.Models.Item;
using PersonalEconomist.Entities.Models.Item;

namespace PersonalEconomist.Services.Stores.ItemStore
{
    public class ItemStore : IItemStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public ItemStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ItemDTO> AddItem(ItemDTO modelDto)
        {
            var model = _mapper.Map<Item>(modelDto);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemDTO>(model);
        }

        public async Task<IEnumerable<ItemDTO>> AllItems()
        {
            var models = _context.Items.ToList();

            return _mapper.Map<IEnumerable<ItemDTO>>(models);
        }

        public async Task<ItemDTO> DeleteItem(Guid Id)
        {
            var model = _context.Items.FirstOrDefault(i => i.Id == Id);
            _context.Items.Remove(model);
            
            await _context.SaveChangesAsync();
            return _mapper.Map<ItemDTO>(model);
        }

        public async Task<ItemDTO> GetItem(Guid Id)
        {
            return _mapper.Map<ItemDTO>(_context.Items.FirstOrDefault(i => i.Id == Id));
        }

        public async Task<ItemDTO> UpdateItem(Guid id, ItemDTO modelDto)
        {
            var model = _mapper.Map<Item>(modelDto);
            model.Id = id;

            _context.Items.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<ItemDTO>(model);
        }


    }
}
