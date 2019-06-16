using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalEconomist.Domain;

namespace PersonalEconomist.Services.Extensions.DbContextProvider
{
    public static class PersonalEconomistDbContextProviderExtension
    {
        public static void AddPersonalEconomistDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<PersonalEconomistDbContext>(provider => provider.UseMySQL(connection));
        }
    }
}
