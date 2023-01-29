﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerApp.Models;

#nullable disable

namespace ServerApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<long?>("SupplierId")
                        .HasColumnType("bigint");

                    b.HasKey("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ServerApp.Models.Rating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RatingId"));

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("ProductId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("ServerApp.Models.Supplier", b =>
                {
                    b.Property<long>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SupplierId"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.HasOne("ServerApp.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ServerApp.Models.Rating", b =>
                {
                    b.HasOne("ServerApp.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("ServerApp.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}