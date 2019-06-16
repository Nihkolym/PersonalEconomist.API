using PersonalEconomist.Entities.Models.Goal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Services.GoalService
{
    public interface IGoalService
    {
        Task<GoalDTO> ReachGoal(Guid goalId, Guid cardId);
    }
}
