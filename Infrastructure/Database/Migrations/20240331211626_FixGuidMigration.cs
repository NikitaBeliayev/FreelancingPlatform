using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixGuidMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //category
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DropForeignKey("FK_CategoryObjective_Category_CategoriesId", "CategoryObjective");
            migrationBuilder.DropColumn("CategoriesId", "CategoryObjective");
            migrationBuilder.DropPrimaryKey("PK_Category", "Category");
            migrationBuilder.DropColumn("Id", "Category");
            migrationBuilder.AddColumn<Guid>("Id", "Category", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_Category", "Category", "Id");
            migrationBuilder.AddColumn<Guid>("CategoriesId", "CategoryObjective", nullable: false);
            migrationBuilder.AddForeignKey("FK_CategoryObjective_Category_CategoriesId", "CategoryObjective", 
                "CategoriesId", "Category", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("888723d5-1e0e-28a2-17a7-2d3759213819"), "java" },
                    { new Guid("8c4f32f0-3202-4af4-9532-a89219192219"), "js" },
                    { new Guid("9fa1efd7-7dbe-7239-fd5a-db6024223d74"), "c#" }
                });
            
            //CommunicationChannel
            migrationBuilder.DeleteData(
                table: "CommunicationChannels",
                keyColumn: "Id",
                keyValue: 1);
            migrationBuilder.DropForeignKey("FK_UserCommunicationChannels_CommunicationChannels_Communicati~", "UserCommunicationChannels");
            migrationBuilder.DropColumn("CommunicationChannelId", "UserCommunicationChannels");
            migrationBuilder.DropPrimaryKey("PK_CommunicationChannels", "CommunicationChannels");
            migrationBuilder.DropColumn("Id", "CommunicationChannels");
            migrationBuilder.AddColumn<Guid>("Id", "CommunicationChannels", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_CommunicationChannels", "CommunicationChannels", "Id");
            migrationBuilder.AddColumn<Guid>("CommunicationChannelId", "UserCommunicationChannels", nullable: false);
            migrationBuilder.AddForeignKey("FK_UserCommunicationChannels_CommunicationChannels_Communicati~", "UserCommunicationChannels", 
                "CommunicationChannelId", "CommunicationChannels", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "CommunicationChannels",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), "Email" });
            
            migrationBuilder.InsertData(
                table: "UserCommunicationChannels",
                columns: new[] { "Id", "UserId", "IsConfirmed", "ConfirmationToken", "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"), new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"), 
                    false, new Guid("9f07d2ac-6009-405d-b329-c517bcc5ef67"), new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.Now });

            migrationBuilder.InsertData(
                table: "UserCommunicationChannels",
                columns: new[] { "Id", "UserId","IsConfirmed","ConfirmationToken", "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"), new Guid("88755139-42b8-415b-84df-04c639d9b47a"), 
                    false, new Guid("6f189094-f7e2-4e40-8d8b-c45054be7b96"), new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.Now });

            migrationBuilder.InsertData(
                table: "UserCommunicationChannels",
                columns: new[] { "Id", "UserId","IsConfirmed","ConfirmationToken", "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"), new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"), 
                    false, new Guid("c24652bd-00bd-48b7-b5e2-59f0094f1e2e"), new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.Now });
            
            
            
            
            //ObjectiveStatus
            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: 5);
            migrationBuilder.DropForeignKey("FK_Objectives_ObjectiveStatus_ObjectiveStatusId", "Objectives");
            migrationBuilder.DropColumn("ObjectiveStatusId", "Objectives");
            migrationBuilder.DropPrimaryKey("PK_ObjectiveStatus", "ObjectiveStatus");
            migrationBuilder.DropColumn("Id", "ObjectiveStatus");
            migrationBuilder.AddColumn<Guid>("Id", "ObjectiveStatus", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_ObjectiveStatus", "ObjectiveStatus", "Id");
            migrationBuilder.AddColumn<Guid>("ObjectiveStatusId", "Objectives", nullable: false);
            migrationBuilder.AddForeignKey("FK_Objectives_ObjectiveStatus_ObjectiveStatusId", "Objectives", "ObjectiveStatusId", "ObjectiveStatus",
                principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "ObjectiveStatus",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("2f2f54aa-46dd-29d0-6459-2afdb5e950ee"), "WaitingForApproval" },
                    { new Guid("327db9d4-0282-c319-b047-dcf22483e225"), "WaitingForAssignment" },
                    { new Guid("6cb13af0-83d5-c772-7ba4-5a3d9a5a1cb9"), "Draft" },
                    { new Guid("c9b0e0b6-fb0c-fedd-767f-137f8066d1df"), "InProgress" },
                    { new Guid("e26529f9-a7c8-b3af-c1b9-a5c09a263636"), "Done" }
                });
            
            //ObjectiveType
            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DropForeignKey("FK_Objectives_ObjectiveType_TypeId", "Objectives");
            migrationBuilder.DropColumn("TypeId", "Objectives");
            migrationBuilder.DropPrimaryKey("PK_ObjectiveType", "ObjectiveType");
            migrationBuilder.DropColumn("Id", "ObjectiveType");
            migrationBuilder.AddColumn<Guid>("Id", "ObjectiveType", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_ObjectiveType", "ObjectiveType", "Id");
            migrationBuilder.AddColumn<Guid>("TypeId", "Objectives", nullable: false);
            migrationBuilder.AddForeignKey("FK_Objectives_ObjectiveType_TypeId", "Objectives", "TypeId", "ObjectiveType",
                principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "ObjectiveType",
                columns: new[] { "Id", "Duration", "Eta", "TypeTitle" },
                values: new object[,]
                {
                    { new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"), 
                        8, new DateTime(2024, 3, 31, 2, 58, 17, 545, DateTimeKind.Local).AddTicks(4057), "Individual" },
                    { new Guid("34719303-dace-07b9-8be3-9a77ee3a48a0"), 
                        8, new DateTime(2024, 3, 31, 2, 58, 17, 545, DateTimeKind.Local).AddTicks(4085), "Group" },
                    { new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"), 
                        8, new DateTime(2024, 3, 31, 2, 58, 17, 545, DateTimeKind.Local).AddTicks(4090), "Team" }
                });
            
            //Payment
            migrationBuilder.DropForeignKey("FK_Objectives_Payment_PaymentId", "Objectives");
            migrationBuilder.DropColumn("PaymentId", "Objectives");
            migrationBuilder.DropPrimaryKey("PK_Payment", "Payment");
            migrationBuilder.DropColumn("Id", "Payment");
            migrationBuilder.AddColumn<Guid>("Id", "Payment", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_Payment", "Payment", "Id");
            migrationBuilder.AddColumn<Guid>("PaymentId", "Objectives", nullable: false);
            migrationBuilder.AddForeignKey("FK_Objectives_Payment_PaymentId", "Objectives", "PaymentId", 
                "Payment", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "Payment", 
                columns: new[] {"Id", "Name"}, 
                values: new object[,]
                {
                    { new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"), "Coin" },
                });
            
            //Role
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 1, new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, new Guid("88755139-42b8-415b-84df-04c639d9b47a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 3, new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5") });
            migrationBuilder.DropForeignKey("FK_RoleUser_Role_RolesId", "RoleUser");
            migrationBuilder.DropColumn("RolesId", "RoleUser");
            migrationBuilder.DropPrimaryKey("PK_Role", "Role");
            migrationBuilder.DropColumn("Id", "Role");
            migrationBuilder.AddColumn<Guid>("Id", "Role", nullable: false);
            migrationBuilder.AddPrimaryKey("PK_Role", "Role", "Id");
            migrationBuilder.AddColumn<Guid>("RolesId", "RoleUser", nullable: false);
            migrationBuilder.AddForeignKey("FK_RoleUser_Role_RolesId", "RoleUser",
                "RolesId", "Role", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), "Admin" },
                    { new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), "Customer" },
                    { new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), "Implementer" }
                });
            migrationBuilder.InsertData(
                table:"RoleUser", 
                columns: new[] {"RolesId", "UsersId"},
                values: new object[,]
                {
                    {new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")},
                    {new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), new Guid("88755139-42b8-415b-84df-04c639d9b47a")},
                    {new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5")}
                });
            
            
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveUser_Objectives_ObjectivesId",
                table: "ObjectiveUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveUser_Users_ImplementorsId",
                table: "ObjectiveUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser");
            

            migrationBuilder.RenameTable(
                name: "ObjectiveUser",
                newName: "ObjectiveImplementors");

            migrationBuilder.RenameColumn(
                name: "ObjectivesId",
                table: "ObjectiveImplementors",
                newName: "ObjectivesToImplementId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjectiveUser_ObjectivesId",
                table: "ObjectiveImplementors",
                newName: "IX_ObjectiveImplementors_ObjectivesToImplementId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommunicationChannelId",
                table: "UserCommunicationChannels",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveImplementors",
                table: "ObjectiveImplementors",
                columns: new[] { "ImplementorsId", "ObjectivesToImplementId" });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4649) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4628) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4646) });
            

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveImplementors_Objectives_ObjectivesToImplementId",
                table: "ObjectiveImplementors",
                column: "ObjectivesToImplementId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveImplementors_Users_ImplementorsId",
                table: "ObjectiveImplementors",
                column: "ImplementorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveImplementors_Objectives_ObjectivesToImplementId",
                table: "ObjectiveImplementors");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveImplementors_Users_ImplementorsId",
                table: "ObjectiveImplementors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveImplementors",
                table: "ObjectiveImplementors");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("888723d5-1e0e-28a2-17a7-2d3759213819"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("8c4f32f0-3202-4af4-9532-a89219192219"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("9fa1efd7-7dbe-7239-fd5a-db6024223d74"));

            migrationBuilder.DeleteData(
                table: "CommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"));

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: new Guid("2f2f54aa-46dd-29d0-6459-2afdb5e950ee"));

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: new Guid("327db9d4-0282-c319-b047-dcf22483e225"));

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: new Guid("6cb13af0-83d5-c772-7ba4-5a3d9a5a1cb9"));

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: new Guid("c9b0e0b6-fb0c-fedd-767f-137f8066d1df"));

            migrationBuilder.DeleteData(
                table: "ObjectiveStatus",
                keyColumn: "Id",
                keyValue: new Guid("e26529f9-a7c8-b3af-c1b9-a5c09a263636"));

            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"));

            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("34719303-dace-07b9-8be3-9a77ee3a48a0"));

            migrationBuilder.DeleteData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"));

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), new Guid("88755139-42b8-415b-84df-04c639d9b47a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5") });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"));

            migrationBuilder.RenameTable(
                name: "ObjectiveImplementors",
                newName: "ObjectiveUser");

            migrationBuilder.RenameColumn(
                name: "ObjectivesToImplementId",
                table: "ObjectiveUser",
                newName: "ObjectivesId");

            migrationBuilder.RenameIndex(
                name: "IX_ObjectiveImplementors_ObjectivesToImplementId",
                table: "ObjectiveUser",
                newName: "IX_ObjectiveUser_ObjectivesId");

            migrationBuilder.AlterColumn<int>(
                name: "CommunicationChannelId",
                table: "UserCommunicationChannels",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "RolesId",
                table: "RoleUser",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Role",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ObjectiveType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ObjectiveStatus",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Objectives",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Objectives",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "ObjectiveStatusId",
                table: "Objectives",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CommunicationChannels",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriesId",
                table: "CategoryObjective",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Category",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser",
                columns: new[] { "ImplementorsId", "ObjectivesId" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "c#" },
                    { 2, "java" },
                    { 3, "js" }
                });

            migrationBuilder.InsertData(
                table: "CommunicationChannels",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Email" });

            migrationBuilder.InsertData(
                table: "ObjectiveStatus",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "InProgress" },
                    { 3, "WaitingForAssignment" },
                    { 4, "WaitingForApproval" },
                    { 5, "Done" }
                });

            migrationBuilder.InsertData(
                table: "ObjectiveType",
                columns: new[] { "Id", "Duration", "Eta", "TypeTitle" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4468), "Individual" },
                    { 2, 8, new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4488), "Group" },
                    { 3, 8, new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4490), "Team" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" },
                    { 3, "Implementer" }
                });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { 1, new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7071) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { 1, new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                columns: new[] { "CommunicationChannelId", "LastEmailSentAt" },
                values: new object[] { 1, new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7069) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { 1, new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca") },
                    { 2, new Guid("88755139-42b8-415b-84df-04c639d9b47a") },
                    { 3, new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveUser_Objectives_ObjectivesId",
                table: "ObjectiveUser",
                column: "ObjectivesId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveUser_Users_ImplementorsId",
                table: "ObjectiveUser",
                column: "ImplementorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
