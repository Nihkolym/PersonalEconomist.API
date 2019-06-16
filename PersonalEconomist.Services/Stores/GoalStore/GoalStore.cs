using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PersonalEconomist.Domain.Models.Goal;
using System.Linq;
using PersonalEconomist.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PersonalEconomist.Services.Stores.GoalStore;
using PersonalEconomist.Entities.Models.Goal;

namespace PersonalEconomist.Services.Stores.GoalStore
{
    public class GoalStore : IGoalStore
    {

        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public GoalStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GoalDTO> AddGoal(GoalDTO goal)
        {
            goal.IsMain = goal.IsMain != null ? goal.IsMain : false;

            var model = _mapper.Map<Goal>(goal);

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<GoalDTO>(model);
        }

        public async Task<IEnumerable<GoalDTO>> GetGoals(string userId)
        {
            return _mapper.Map<List<GoalDTO>>(_context.Goals.Where(g => g.UserId == userId));
        }

        public async Task<GoalDTO> UpdateGoal(Guid id, GoalDTO modelDto)
        {
            var model = _mapper.Map<Goal>(modelDto);
            model.Id = id;

            _context.Goals.Update(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<GoalDTO>(model);
        }

        public async Task<GoalDTO> DeleteGoal(Guid Id)
        {
            var model = _context.Goals.FirstOrDefault(g => g.Id == Id);
            _context.Goals.Remove(model);

            await _context.SaveChangesAsync();
            return _mapper.Map<GoalDTO>(model);
        }

        public async Task<GoalDTO> GetGoal(Guid Id)
        {
            return _mapper.Map<GoalDTO>(_context.Goals.FirstOrDefault(g => g.Id == Id));
        }
    }
}
