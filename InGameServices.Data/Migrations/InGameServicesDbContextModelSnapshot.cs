﻿// <auto-generated />
using System;
using InGameServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InGameServices.Data.Migrations
{
    [DbContext(typeof(InGameServicesDbContext))]
    partial class InGameServicesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("InGameServices.Data.Entities.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<string>("MainPictureUrl")
                        .HasColumnType("longtext")
                        .HasColumnName("MainPictureUrl");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("Price");

                    b.Property<string>("Title")
                        .HasColumnType("longtext")
                        .HasColumnName("Title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.ServiceAccess", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("ServiceAccess");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.ServiceRating", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext")
                        .HasColumnName("Comment");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("Rating");

                    b.HasKey("UserId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceRating");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("Password");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("longtext")
                        .HasColumnName("PictureUrl");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.Service", b =>
                {
                    b.HasOne("InGameServices.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.ServiceAccess", b =>
                {
                    b.HasOne("InGameServices.Data.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InGameServices.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InGameServices.Data.Entities.ServiceRating", b =>
                {
                    b.HasOne("InGameServices.Data.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InGameServices.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}