using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalEconomist.Services.Services.AuthService;
using PersonalEconomist.Services.Services.TokenService;
using PersonalEconomist.Services.Stores.GoalStore;
using PersonalEconomist.Services.Stores.ItemStore;
using PersonalEconomist.Services.Stores.TransactionStore;
using PersonalEconomist.Services.Stores.ActivityStore;
using PersonalEconomist.Services.Services.FileService;
using PersonalEconomist.Services.Stores.CreditCardStore;
using PersonalEconomist.Services.Services.CreditCardService;
using PersonalEconomist.Services.Stores.UserStore;
using PersonalEconomist.Services.Services.GoalService;
using PersonalEconomist.Services.Stores.IndicationStore;
using PersonalEconomist.Services.Stores.CounterStore;

namespace PersonalEconomist.WebAPI.Extensions
{
    public static class ServiceComponentsDI
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IActivityStore, ActivityStore>();
            services.AddTransient<IGoalStore, GoalStore>();
            services.AddTransient<IItemStore, ItemStore>();
            services.AddTransient<ITransactionStore, TransactionStore>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ICreditCardStore, CreditCardStore>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<IUserStore, UserStore>();
            services.AddTransient<IGoalService, GoalService>();
            services.AddTransient<IIndicationStore, IndicationStore>();
            services.AddTransient<ICounterStore, CounterStore>();
        }
    }
}
