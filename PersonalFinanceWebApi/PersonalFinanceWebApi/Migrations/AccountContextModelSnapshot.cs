﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalFinanceWebApi.Models;

namespace PersonalFinanceWebApi.Migrations
{
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("PersonalFinanceWebApi.Models.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("LatestDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AccountItems");
                });

            modelBuilder.Entity("PersonalFinanceWebApi.Models.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Desc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<float>("amount")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("TransactionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
