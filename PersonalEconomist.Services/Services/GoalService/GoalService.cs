using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersonalEconomist.Domain;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.Goal;
using PersonalEconomist.Services.Stores.GoalStore;
using PersonalEconomist.Services.Stores.UserStore;
using System.Linq;
using PersonalEconomist.Services.Services.CreditCardService;
using Microsoft.EntityFrameworkCore;
using PersonalEconomist.Domain.Models.Goal;

namespace PersonalEconomist.Services.Services.GoalService
{
    public class GoalService : IGoalService
    {
        private readonly IMapper _mapper;
        private readonly PersonalEconomistDbContext _context;
        private readonly IGoalStore _goalStore;
        private readonly IUserStore _userStore;
        private readonly ICreditCardService _creditCardService;

        public GoalService(
            PersonalEconomistDbContext context,
            IMapper mapper,
            IGoalStore goalStore,
            IUserStore userStore,
            ICreditCardService creditCardService
        )
        {
            _mapper = mapper;
            _context = context;
            _goalStore = goalStore;
            _userStore = userStore;
            _creditCardService = creditCardService;
        }

        public async Task<GoalDTO> ReachGoal(Guid goalId, Guid cardId)
        {
            var goal = _context.Goals.FirstOrDefault(g => g.Id == goalId);

            var user = _context.Users.FirstOrDefault(u => u.Id == goal.UserId);

            var card = _context.CreditCards.AsNoTracking().Where(c => c.Id == cardId).FirstOrDefault();

            if (user.Amount < goal.Amount || card == null)
            {
                throw new InvalidOperationException();
            }

            using (var _transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    user.Amount = user.Amount - goal.Amount;

                    _context.Users.Update(_mapper.Map<User>(user));

                    goal.IsDeleted = true;

                    _context.Goals.Update(_mapper.Map<Goal>(goal));

                    await _context.SaveChangesAsync();

                    await _creditCardService.Replenish(cardId, goal.Amount);

                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
            }

            return _mapper.Map<GoalDTO>(_context.Goals.Where(g => g.Id == goalId).FirstOrDefault());
        }
    }
}
