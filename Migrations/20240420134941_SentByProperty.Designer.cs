﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UserDataService.DataContext;

#nullable disable

namespace UserDataService.Migrations
{
    [DbContext(typeof(UserDataContext))]
    [Migration("20240420134941_SentByProperty")]
    partial class SentByProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UserDataService.DataContext.Entities.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FriendId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<int>("SentBy")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId1")
                        .HasColumnType("integer");

                    b.Property<double>("WinrateAgainst")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BlackId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("Moves")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int?>("StatisticsId")
                        .HasColumnType("integer");

                    b.Property<int>("WhiteId")
                        .HasColumnType("integer");

                    b.Property<string>("Win")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BlackId");

                    b.HasIndex("StatisticsId");

                    b.HasIndex("WhiteId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Statistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<double>("WinrateAI")
                        .HasColumnType("double precision");

                    b.Property<double>("WinrateFriends")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Friendship", b =>
                {
                    b.HasOne("UserDataService.DataContext.Entities.User", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("UserDataService.DataContext.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("UserDataService.DataContext.Entities.User", null)
                        .WithMany("Friendships")
                        .HasForeignKey("UserId1");

                    b.Navigation("Friend");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Game", b =>
                {
                    b.HasOne("UserDataService.DataContext.Entities.User", "Black")
                        .WithMany()
                        .HasForeignKey("BlackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserDataService.DataContext.Entities.Statistics", null)
                        .WithMany("Games")
                        .HasForeignKey("StatisticsId");

                    b.HasOne("UserDataService.DataContext.Entities.User", "White")
                        .WithMany()
                        .HasForeignKey("WhiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Black");

                    b.Navigation("White");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Statistics", b =>
                {
                    b.HasOne("UserDataService.DataContext.Entities.User", null)
                        .WithOne("Statistics")
                        .HasForeignKey("UserDataService.DataContext.Entities.Statistics", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.Statistics", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("UserDataService.DataContext.Entities.User", b =>
                {
                    b.Navigation("Friendships");

                    b.Navigation("Statistics")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
