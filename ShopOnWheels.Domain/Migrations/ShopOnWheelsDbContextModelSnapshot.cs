﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopOnWheels.Domain;

namespace ShopOnWheels.Domain.Migrations
{
    [DbContext(typeof(ShopOnWheelsDbContext))]
    partial class ShopOnWheelsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ShopOnWheels.Domain.Models.Order.Product", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<DateTime>("CreatedOn");

                    b.Property<short>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Manufacturer")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}