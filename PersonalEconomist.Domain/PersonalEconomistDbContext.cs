using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonalEconomist.Domain.Models.Base;
using PersonalEconomist.Domain.Models.CreditCard;
using PersonalEconomist.Domain.Models.Goal;
using PersonalEconomist.Domain.Models.Activity;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Domain.Models.Item;
using PersonalEconomist.Domain.Models.Transaction;
using PersonalEconomist.Domain.Models.TransactionItem;
using PersonalEconomist.Domain.Tools;
using PersonalEconomist.Domain.Models.Counter;
using PersonalEconomist.Domain.Models.Indication;

namespace PersonalEconomist.Domain
{
    public class PersonalEconomistDbContext : IdentityDbContext<User>
    {
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Indication> Indications { get; set; }

        public PersonalEconomistDbContext(DbContextOptions<PersonalEconomistDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>().HasIndex(i => i.Title).IsUnique();

            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.ProviderKey).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.LoginProvider).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens")//I have to declare the table name, otherwise IdentityUser will be created
               .Property(c => c.LoginProvider).HasMaxLength(36).IsRequired();

            builder.Entity<IdentityRole>()
               .Property(c => c.Name).HasMaxLength(36).IsRequired();

            builder.Entity<User>()
              .Property(c => c.UserName).HasMaxLength(36).IsRequired();

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }

            builder.Entity<User>()
               .Property(user => user.Email)
               .HasMaxLength(36);

            builder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            builder.Entity<User>()
               .Property(p => p.LockoutEnabled)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.TwoFactorEnabled)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.EmailConfirmed)
               .HasColumnType("bit");

            builder.Entity<User>()
               .Property(p => p.PhoneNumberConfirmed)
               .HasColumnType("bit");

            builder.Entity<Goal>()
                .Property(g => g.IsDeleted)
                .HasColumnType("bit");

            builder.Entity<CreditCard>()
                .Property(c => c.IsDeleted)
                .HasColumnType("bit");

            builder.Entity<TransactionItem>()
               .Property(ti => ti.IsDeleted)
               .HasColumnType("bit");

            builder.Entity<Transaction>()
               .Property(t => t.IsDeleted)
               .HasColumnType("bit");

            builder.Entity<Item>()
               .Property(i => i.IsDeleted)
               .HasColumnType("bit");

            builder.Entity<Goal>()
               .Property(g => g.IsMain)
               .HasColumnType("bit");


            builder.Entity<Activity>()
               .Property(a => a.IsDeleted)
               .HasColumnType("bit");

            builder.Entity<Counter>()
              .Property(a => a.IsDeleted)
              .HasColumnType("bit");

            builder.Entity<Indication>()
              .Property(a => a.IsDeleted)
              .HasColumnType("bit");

            base.OnModelCreating(builder);

        }
    }
}

