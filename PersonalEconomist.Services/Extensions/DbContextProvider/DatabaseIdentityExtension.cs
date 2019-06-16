using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PersonalEconomist.Domain;
using Microsoft.Extensions.Identity;
using PersonalEconomist.Domain.Models.User;

namespace PersonalEconomist.Services.Extensions.DbContextProvider
{
    public static class DatabaseIdentityExtension
    {
        public static void SetupIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opts => {
                opts.Password.RequiredLength = 5;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуютс
            })
                .AddEntityFrameworkStores<PersonalEconomistDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();
        }
    }
}
