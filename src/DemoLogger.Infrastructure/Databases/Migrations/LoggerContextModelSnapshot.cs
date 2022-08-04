﻿// <auto-generated />
using DemoLogger.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoLogger.Infrastructure.Databases.Migrations
{
    [DbContext(typeof(LoggerContext))]
    partial class LoggerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Logger.Domain.Aggregates.Logger.LoggerInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("logger", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}