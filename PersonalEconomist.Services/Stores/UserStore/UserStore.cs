using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersonalEconomist.Domain;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.User;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PersonalEconomist.Services.Stores.UserStore
{
    public class UserStore : IUserStore
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;

        public UserStore(IMapper mapper, PersonalEconomistDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<User> GetUser(string userId)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId);
        }

        public async Task<UserDTO> UpdateUser(string userId, UserDTO modelDto)
        {
            var user = await GetUser(userId);
            if (user.Avatar != modelDto.Avatar)
            {
                user.Avatar = modelDto.Avatar;
            }
            if (modelDto.UserName != null && user.UserName != modelDto.UserName)
            {
                user.UserName = modelDto.UserName;
            }

            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }
    }
}
