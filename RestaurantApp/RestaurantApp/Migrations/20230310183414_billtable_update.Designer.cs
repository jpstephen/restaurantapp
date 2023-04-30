﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantApp.Data;

#nullable disable

namespace RestaurantApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230310183414_billtable_update")]
    partial class billtable_update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantApp.Models.Bills", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillId"));

                    b.Property<string>("addDeliveryInBill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("addMobileNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("addSignatureInBill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("billDeleted")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("deletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("itemsadded")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BillId");

                    b.ToTable("allBills");
                });

            modelBuilder.Entity("RestaurantApp.Models.Items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("MItems");
                });
#pragma warning restore 612, 618
        }
    }
}