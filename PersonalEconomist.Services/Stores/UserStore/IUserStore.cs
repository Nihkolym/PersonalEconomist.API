using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.UserStore
{
    public interface IUserStore
    {
        Task<UserDTO> UpdateUser(string userId, UserDTO model);
        Task<User> GetUser(string userId);
    }
}
