﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240316132045_AddCreatorPublicContacts")]
    partial class AddCreatorPublicContacts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryObjective", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ObjectivesId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "ObjectivesId");

                    b.HasIndex("ObjectivesId");

                    b.ToTable("CategoryObjective");
                });

            modelBuilder.Entity("Domain.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Domain.CommunicationChannels.CommunicationChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CommunicationChannels");

                    b.HasData(
                        new
                        {
                            Id = 1,
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

                    b.Property<string>("CreatorPublicContacts")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ObjectiveStatusId")
                        .HasColumnType("integer");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveStatusId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("Domain.Payments.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Implementer"
                        });
                });

            modelBuilder.Entity("Domain.Statuses.ObjectiveStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectiveStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Draft"
                        },
                        new
                        {
                            Id = 2,
                            Title = "InProgress"
                        },
                        new
                        {
                            Id = 3,
                            Title = "WaitingForAssignment"
                        },
                        new
                        {
                            Id = 4,
                            Title = "WaitingForApproval"
                        },
                        new
                        {
                            Id = 5,
                            Title = "Done"
                        });
                });

            modelBuilder.Entity("Domain.Types.ObjectiveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Eta")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TypeTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectiveType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duration = 8,
                            Eta = new DateTime(2024, 3, 18, 15, 20, 45, 350, DateTimeKind.Local).AddTicks(3567),
                            TypeTitle = "Individual"
                        },
                        new
                        {
                            Id = 2,
                            Duration = 8,
                            Eta = new DateTime(2024, 3, 18, 15, 20, 45, 350, DateTimeKind.Local).AddTicks(3632),
                            TypeTitle = "Group"
                        },
                        new
                        {
                            Id = 3,
                            Duration = 8,
                            Eta = new DateTime(2024, 3, 18, 15, 20, 45, 350, DateTimeKind.Local).AddTicks(3637),
                            TypeTitle = "Team"
                        });
                });

            modelBuilder.Entity("Domain.UserCommunicationChannels.UserCommunicationChannel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CommunicationChannelId")
                        .HasColumnType("integer");

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
                            CommunicationChannelId = 1,
                            ConfirmationToken = new Guid("9f07d2ac-6009-405d-b329-c517bcc5ef67"),
                            IsConfirmed = false,
                            LastEmailSentAt = new DateTime(2024, 3, 16, 13, 20, 45, 351, DateTimeKind.Utc).AddTicks(4882),
                            UserId = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")
                        },
                        new
                        {
                            Id = new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                            CommunicationChannelId = 1,
                            ConfirmationToken = new Guid("6f189094-f7e2-4e40-8d8b-c45054be7b96"),
                            IsConfirmed = false,
                            LastEmailSentAt = new DateTime(2024, 3, 16, 13, 20, 45, 351, DateTimeKind.Utc).AddTicks(4898),
                            UserId = new Guid("88755139-42b8-415b-84df-04c639d9b47a")
                        },
                        new
                        {
                            Id = new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                            CommunicationChannelId = 1,
                            ConfirmationToken = new Guid("c24652bd-00bd-48b7-b5e2-59f0094f1e2e"),
                            IsConfirmed = false,
                            LastEmailSentAt = new DateTime(2024, 3, 16, 13, 20, 45, 351, DateTimeKind.Utc).AddTicks(4901),
                            UserId = new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5")
                        });
                });

            modelBuilder.Entity("Domain.Users.UserDetails.User", b =>
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
                    b.Property<Guid>("ObjectivesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("ObjectivesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ObjectiveUser");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");

                    b.HasData(
                        new
                        {
                            RolesId = 1,
                            UsersId = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")
                        },
                        new
                        {
                            RolesId = 2,
                            UsersId = new Guid("88755139-42b8-415b-84df-04c639d9b47a")
                        },
                        new
                        {
                            RolesId = 3,
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

                    b.HasOne("Domain.Users.UserDetails.User", "User")
                        .WithMany("CommunicationChannels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommunicationChannel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ObjectiveUser", b =>
                {
                    b.HasOne("Domain.Objectives.Objective", null)
                        .WithMany()
                        .HasForeignKey("ObjectivesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.UserDetails.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
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

                    b.HasOne("Domain.Users.UserDetails.User", null)
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

            modelBuilder.Entity("Domain.Users.UserDetails.User", b =>
                {
                    b.Navigation("CommunicationChannels");
                });
#pragma warning restore 612, 618
        }
    }
}
