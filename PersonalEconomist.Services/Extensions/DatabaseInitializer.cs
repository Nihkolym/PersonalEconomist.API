using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalEconomist.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PersonalEconomist.Services.Helpers;
using PersonalEconomist.Domain.Identity;
using PersonalEconomist.Domain.Models.User;
using System.Linq;

namespace PersonalEconomist.Services.Extensions
{
    public static class DatabaseInitializer
    {

        public static async Task EnsureDatabaseInitialized(IServiceScope serviceScope)
        {
            PersonalEconomistDbContext context = serviceScope.ServiceProvider.GetRequiredService<PersonalEconomistDbContext>();

            bool isFirstLaunch = !(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

            context.Database.Migrate();

            if (isFirstLaunch)
            {
                await SeedRoles(context);
                await SeedAdmin(serviceScope);
                await AddTestData(context);
            }

            await context.SaveChangesAsync();
            context.Dispose();
        }

        public static async Task SeedRoles(PersonalEconomistDbContext context)
        {
            var userRole = Roles.User;
            var adminRole = Roles.Admin;

            await context.AddRangeAsync(
                new IdentityRole[] {
                    new IdentityRole { Name = userRole, NormalizedName = userRole.ToUpper() },
                    new IdentityRole { Name = adminRole, NormalizedName = adminRole.ToUpper() }
                }
            );
        }

        public static async Task SeedAdmin(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            const string email = Constants.Strings.Admin.Email;
            const string password = Constants.Strings.Admin.Password;
            const string avatar = "panda.gif";
            var role = Roles.Admin;

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                User user = new User
                {
                    Id = "a13a2c82-f180-486b-9df0-3386bb634ee8",
                    UserName = email,
                    Email = email,
                    Avatar = avatar,
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }

        }

        public static async Task WarmUpContext(IServiceScope serviceScope)
        {
            PersonalEconomistDbContext context = serviceScope.ServiceProvider.GetRequiredService<PersonalEconomistDbContext>();

            await context.Users
                .Include(u => u.Goals)
                .FirstOrDefaultAsync();

            await context.TransactionItems
               .Include(ti => ti.Transaction)
               .FirstOrDefaultAsync();
        }

        public static async Task AddTestData(PersonalEconomistDbContext context)
        {
            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `activities` (`Id`, `CreatedOn`, `Image`, `IsDeleted`, `ModifiedOn`, `Title`) VALUES
                    ('aab99ed6-0c-4d0e', '2018-05-21 18:25:38.242873', 'food.png', b'0', '2018-05-21 18:25:38.242873', 'Food'),
                    ('fb98d45d-2e-47ec', '2018-05-21 18:25:53.250389', 'drink.png', b'0', '2018-05-21 18:25:53.250389', 'Drink'),
                    ('013fc2a8-e5-42cd', '2018-05-21 18:25:53.250389', 'shower.png', b'0', '2018-05-21 18:25:53.250389', 'Shower');");

            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `items` (`Id`, `CreatedOn`, `Image`, `IsDeleted`, `ModifiedOn`, `Title`, `Price`, `ActivityId`) VALUES
                    ('67b4ee67-d0-4b83', '2018-05-21 18:25:38.242873', 'coca-cola.png', b'0', '2018-05-21 18:25:38.242873', 'Coca-cola', 100, 'fb98d45d-2e-47ec'),
                    ('fb98d45d-2e-47ec', '2018-05-21 18:25:38.242873', 'pepsi.png', b'0', '2018-05-21 18:25:38.242873', 'Pepsi', 50, 'fb98d45d-2e-47ec'),
                    ('013fc2a8-e5-42cd', '2018-05-21 18:25:38.242873', 'pizza.png', b'0', '2018-05-21 18:25:38.242873', 'Pizza', 250, 'aab99ed6-0c-4d0e'),
                    ('81eea949-d5-4f83', '2018-05-21 18:25:38.242873', 'burger.png', b'0', '2018-05-21 18:25:38.242873', 'Burger', 200, 'aab99ed6-0c-4d0e'),
                    ('81eea949-d5-4f82', '2018-05-21 18:25:38.242873', 'shampoo.png', b'0', '2018-05-21 18:25:38.242873', 'Shampoo', 150, '013fc2a8-e5-42cd');");

            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `creditcards` (`Id`, `CreatedOn`, `UserId`, `IsDeleted`, `ModifiedOn`, `Amount`, `CardNumber`, `PinCode`) VALUES
                    ('1f57cffe-43-428b', '2018-05-21 18:25:38.242873', 'a13a2c82-f180-486b-9df0-3386bb634ee8', b'0', '2018-05-21 18:25:38.242873', 300, '4111 1111 1111 1111', '1234567'),
                    ('1f57cffe-43-429b', '2018-05-21 18:25:53.250389', 'a13a2c82-f180-486b-9df0-3386bb634ee8', b'0', '2018-05-21 18:25:53.250389', 500, '4111 1111 1111 1112', '123456');");

            await context.Database.ExecuteSqlCommandAsync($@"INSERT INTO `goals` (`Id`, `CreatedOn`, `UserId`, `IsDeleted`, `ModifiedOn`, `Amount`, `Title`, `Image`, `IsMain`) VALUES
                    ('81eea949-d5-4f8c', '2018-05-21 18:25:53.250389', 'a13a2c82-f180-486b-9df0-3386bb634ee8', b'0', '2018-05-21 18:25:53.250389', 1000, 'Buy IPhone', 'iphone.png', b'1');");
        }
    }
}
