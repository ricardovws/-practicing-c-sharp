﻿// <auto-generated />
using System;
using ArtworkManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArtworkManager.Migrations
{
    [DbContext(typeof(ArtworkManagerContext))]
    partial class ArtworkManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ArtworkManager.Models.Artwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Code");

                    b.Property<int>("OwnerID");

                    b.Property<string>("PublicationCode");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Artwork");
                });

            modelBuilder.Entity("ArtworkManager.Models.ArtworkCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtworkCodeCode");

                    b.HasKey("Id");

                    b.ToTable("ArtworkCode");
                });

            modelBuilder.Entity("ArtworkManager.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<int>("TeamId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("ArtworkManager.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ArtworkManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Admin");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ArtworkManager.Models.Artwork", b =>
                {
                    b.HasOne("ArtworkManager.Models.Author", "Owner")
                        .WithMany("Artworks")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ArtworkManager.Models.Author", b =>
                {
                    b.HasOne("ArtworkManager.Models.Team", "Team")
                        .WithMany("Authors")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ArtworkManager.Models.User", b =>
                {
                    b.HasOne("ArtworkManager.Models.Author", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
