﻿// <auto-generated />
using System;
using AngelCo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AngelCo.Domain.Migrations
{
    [DbContext(typeof(DomainContext))]
    partial class DomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AngelCo.Domain.Education", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DegreeType")
                        .HasMaxLength(200);

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("FullDegreeName")
                        .HasMaxLength(200);

                    b.Property<int?>("GraduationMonth");

                    b.Property<int?>("GraduationYear");

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("AngelCo.Domain.Experience", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .HasMaxLength(200);

                    b.Property<int?>("EndedAtMonth");

                    b.Property<int?>("EndedAtYear");

                    b.Property<string>("Role")
                        .HasMaxLength(200);

                    b.Property<int?>("StartAtMonth");

                    b.Property<int?>("StartAtYear");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("AngelCo.Domain.Location", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("AngelCo.Domain.Market", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(200);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("AngelCo.Domain.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Bio");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FacebookUrl")
                        .HasMaxLength(500);

                    b.Property<string>("FullName")
                        .HasMaxLength(300);

                    b.Property<string>("LinkedInUrl")
                        .HasMaxLength(500);

                    b.Property<string>("ProfileLink")
                        .HasMaxLength(500);

                    b.Property<string>("TwitterUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Type")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AngelCo.Domain.Education", b =>
                {
                    b.HasOne("AngelCo.Domain.User", "User")
                        .WithMany("Educations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngelCo.Domain.Experience", b =>
                {
                    b.HasOne("AngelCo.Domain.User", "User")
                        .WithMany("Experiences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngelCo.Domain.Location", b =>
                {
                    b.HasOne("AngelCo.Domain.User", "User")
                        .WithMany("Locations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AngelCo.Domain.Market", b =>
                {
                    b.HasOne("AngelCo.Domain.User", "User")
                        .WithMany("Markets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}