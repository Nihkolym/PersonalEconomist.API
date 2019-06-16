using AutoMapper;
using PersonalEconomist.Domain;
using PersonalEconomist.Entities.Models.Activity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PersonalEconomist.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PersonalEconomist.Services.Stores.ActivityStore
{
    public class ActivityStore : IActivityStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public ActivityStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActivityDTO> AddActivity(ActivityDTO activity)
        {
            var model = _mapper.Map<Domain.Models.Activity.Activity>(activity);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<ActivityDTO>(model);
        }

        public async Task<IEnumerable<ActivityDTO>> GetActivities()
        {
            return _mapper.Map<List<ActivityDTO>>(_context.Activities.Include(activity => activity.Items).AsEnumerable());

        }

        public async Task<ActivityDTO> UpdateActivity(Guid id, ActivityDTO modelDto)
        {
            var model = _mapper.Map<Domain.Models.Activity.Activity>(modelDto);
            model.Id = id;

            _context.Activities.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<ActivityDTO>(model);
        }

        public async Task<ActivityDTO> DeleteActivity(Guid Id)
        {
            var model = _context.Activities.FirstOrDefault(a => a.Id == Id);
            _context.Activities.Remove(model);

            await _context.SaveChangesAsync();
            return _mapper.Map<ActivityDTO>(model);
        }

        public async Task<ActivityDTO> GetActivity(Guid Id)
        {
            return _mapper.Map<ActivityDTO>(_context.Activities.FirstOrDefault(a => a.Id == Id));
        }
    }
}
