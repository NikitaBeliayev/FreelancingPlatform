﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryObjective", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ObjectivesId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "ObjectivesId");

                    b.HasIndex("ObjectivesId");

                    b.ToTable("CategoryObjective");
                });

            modelBuilder.Entity("Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9fa1efd7-7dbe-7239-fd5a-db6024223d74"),
                            Title = "c#"
                        },
                        new
                        {
                            Id = new Guid("888723d5-1e0e-28a2-17a7-2d3759213819"),
                            Title = "java"
                        },
                        new
                        {
                            Id = new Guid("8c4f32f0-3202-4af4-9532-a89219192219"),
                            Title = "js"
                        });
                });

            modelBuilder.Entity("Domain.CommunicationChannels.CommunicationChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CommunicationChannels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"),
                            Name = "Email"
                        });
                });

            modelBuilder.Entity("Domain.Objectives.Objective", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Attachments")
                        .HasColumnType("bytea");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatorPublicContacts")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Eta")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ObjectiveStatusId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ObjectiveStatusId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("Domain.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Payment");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"),
                            Name = "Domain.Payments.PaymentName"
                        });
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"),
                            Name = "Customer"
                        },
                        new
                        {
                            Id = new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"),
                            Name = "Implementer"
                        });
                });

            modelBuilder.Entity("Domain.Statuses.ObjectiveStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectiveStatus");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6cb13af0-83d5-c772-7ba4-5a3d9a5a1cb9"),
                            Title = "Draft"
                        },
                        new
                        {
                            Id = new Guid("c9b0e0b6-fb0c-fedd-767f-137f8066d1df"),
                            Title = "InProgress"
                        },
                        new
                        {
                            Id = new Guid("327db9d4-0282-c319-b047-dcf22483e225"),
                            Title = "WaitingForAssignment"
                        },
                        new
                        {
                            Id = new Guid("2f2f54aa-46dd-29d0-6459-2afdb5e950ee"),
                            Title = "WaitingForApproval"
                        },
                        new
                        {
                            Id = new Guid("e26529f9-a7c8-b3af-c1b9-a5c09a263636"),
                            Title = "Done"
                        });
                });

            modelBuilder.Entity("Domain.Types.ObjectiveType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("TypeTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectiveType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"),
                            Duration = 8,
                            TypeTitle = "Individual"
                        },
                        new
                        {
                            Id = new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"),
                            Duration = 8,
                            TypeTitle = "Team"
                        });
                });

            modelBuilder.Entity("Domain.UserCommunicationChannels.UserCommunicationChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommunicationChannelId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConfirmationToken")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastEmailSentAt")
                        .IsRequired()
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CommunicationChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCommunicationChannels");

                    b.HasData(
                        new
                        {
                            Id = new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                            CommunicationChannelId = new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"),
                            ConfirmationToken = new Guid("9f07d2ac-6009-405d-b329-c517bcc5ef67"),
                            IsConfirmed = true,
                            LastEmailSentAt = new DateTime(2024, 5, 19, 17, 7, 24, 66, DateTimeKind.Utc).AddTicks(1595),
                            UserId = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")
                        },
                        new
                        {
                            Id = new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                            CommunicationChannelId = new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"),
                            ConfirmationToken = new Guid("6f189094-f7e2-4e40-8d8b-c45054be7b96"),
                            IsConfirmed = true,
                            LastEmailSentAt = new DateTime(2024, 5, 19, 17, 7, 24, 66, DateTimeKind.Utc).AddTicks(1606),
                            UserId = new Guid("88755139-42b8-415b-84df-04c639d9b47a")
                        },
                        new
                        {
                            Id = new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                            CommunicationChannelId = new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"),
                            ConfirmationToken = new Guid("c24652bd-00bd-48b7-b5e2-59f0094f1e2e"),
                            IsConfirmed = true,
                            LastEmailSentAt = new DateTime(2024, 5, 19, 17, 7, 24, 66, DateTimeKind.Utc).AddTicks(1610),
                            UserId = new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5")
                        });
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"),
                            Email = "testadmin@gmail.com",
                            FirstName = "User1",
                            LastName = "Admin",
                            Password = "9259de5682f80ba967a5263d420f44bb40a9267f9787d8034d597a69439e075f"
                        },
                        new
                        {
                            Id = new Guid("88755139-42b8-415b-84df-04c639d9b47a"),
                            Email = "testcustomer@gmail.com",
                            FirstName = "User2",
                            LastName = "Customer",
                            Password = "afed0e5dd16c3aa13c0913df9557fe7ff05129a2eb4bf9c54b2c68545aec63b1"
                        },
                        new
                        {
                            Id = new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"),
                            Email = "testimplementer@gmail.com",
                            FirstName = "User3",
                            LastName = "Implementer",
                            Password = "d7b810563cf203ede3043fde799c9705b9ca66635c68cf0465ac3259200f59fe"
                        });
                });

            modelBuilder.Entity("ObjectiveUser", b =>
                {
                    b.Property<Guid>("ImplementorsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ObjectivesToImplementId")
                        .HasColumnType("uuid");

                    b.HasKey("ImplementorsId", "ObjectivesToImplementId");

                    b.HasIndex("ObjectivesToImplementId");

                    b.ToTable("ObjectiveImplementors", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RolesId = new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"),
                            UsersId = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")
                        },
                        new
                        {
                            RolesId = new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"),
                            UsersId = new Guid("88755139-42b8-415b-84df-04c639d9b47a")
                        },
                        new
                        {
                            RolesId = new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"),
                            UsersId = new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5")
                        });
                });

            modelBuilder.Entity("CategoryObjective", b =>
                {
                    b.HasOne("Domain.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Objectives.Objective", null)
                        .WithMany()
                        .HasForeignKey("ObjectivesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Objectives.Objective", b =>
                {
                    b.HasOne("Domain.Users.User", "Creator")
                        .WithMany("CreatedObjectives")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Statuses.ObjectiveStatus", "ObjectiveStatus")
                        .WithMany("Objectives")
                        .HasForeignKey("ObjectiveStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Payments.Payment", "Payment")
                        .WithMany("Objectives")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Types.ObjectiveType", "Type")
                        .WithMany("Objectives")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("ObjectiveStatus");

                    b.Navigation("Payment");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.UserCommunicationChannels.UserCommunicationChannel", b =>
                {
                    b.HasOne("Domain.CommunicationChannels.CommunicationChannel", "CommunicationChannel")
                        .WithMany("UserCommunicationChannels")
                        .HasForeignKey("CommunicationChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.User", "User")
                        .WithMany("CommunicationChannels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommunicationChannel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ObjectiveUser", b =>
                {
                    b.HasOne("Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("ImplementorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Objectives.Objective", null)
                        .WithMany()
                        .HasForeignKey("ObjectivesToImplementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Domain.Roles.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.CommunicationChannels.CommunicationChannel", b =>
                {
                    b.Navigation("UserCommunicationChannels");
                });

            modelBuilder.Entity("Domain.Payments.Payment", b =>
                {
                    b.Navigation("Objectives");
                });

            modelBuilder.Entity("Domain.Statuses.ObjectiveStatus", b =>
                {
                    b.Navigation("Objectives");
                });

            modelBuilder.Entity("Domain.Types.ObjectiveType", b =>
                {
                    b.Navigation("Objectives");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("CommunicationChannels");

                    b.Navigation("CreatedObjectives");
                });
#pragma warning restore 612, 618
        }
    }
}
