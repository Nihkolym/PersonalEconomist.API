using PersonalEconomist.Entities.Models.Goal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.GoalStore
{
    public interface IGoalStore
    {
        Task<IEnumerable<GoalDTO>> GetGoals(string userId);
        Task<GoalDTO> AddGoal(GoalDTO goal);
        Task<GoalDTO> UpdateGoal(Guid id, GoalDTO modelDto);
        Task<GoalDTO> DeleteGoal(Guid id);
        Task<GoalDTO> GetGoal(Guid Id);
    }
}
