using PersonalEconomist.Entities.Models.Activity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.ActivityStore
{
    public interface IActivityStore
    {
        Task<IEnumerable<ActivityDTO>> GetActivities();
        Task<ActivityDTO> AddActivity(ActivityDTO activity);
        Task<ActivityDTO> UpdateActivity(Guid id, ActivityDTO modelDto);
        Task<ActivityDTO> DeleteActivity(Guid id);
        Task<ActivityDTO> GetActivity(Guid Id);
    }
}
