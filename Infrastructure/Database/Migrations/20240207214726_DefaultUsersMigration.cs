using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DefaultUsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 2, 9, 23, 47, 26, 157, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 2, 9, 23, 47, 26, 157, DateTimeKind.Local).AddTicks(7789));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 2, 9, 23, 47, 26, 157, DateTimeKind.Local).AddTicks(7793));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"), "testimplementer@gmail.com", "User3", "Implementer", "f36711e3bac2be05f53c12f7722279162bd1ecb945a331db5cb1bed89e2d298f" },
                    { new Guid("88755139-42b8-415b-84df-04c639d9b47a"), "testcustomer@gmail.com", "User2", "Customer", "005ecd48c62dfa2d5d2824958a2ac400fbd82b8e94a8fc5f9fcc9c091a1c7d39" },
                    { new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"), "testadmin@gmail.com", "User1", "Admin", "3c7e2f8d6920bc72da2b24e9d47ca302ce56b6a047ff70d352294bbaf2bf3054" }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { 1, new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca") },
                    { 2, new Guid("88755139-42b8-415b-84df-04c639d9b47a") },
                    { 3, new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5") }
                });

            migrationBuilder.InsertData(
                table: "UserCommunicationChannels",
                columns: new[] { "Id", "CommunicationChannelId", "ConfirmationToken", "IsConfirmed", "UserId" },
                values: new object[,]
                {
                    { new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"), 1, new Guid("c24652bd-00bd-48b7-b5e2-59f0094f1e2e"), false, new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5") },
                    { new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"), 1, new Guid("9f07d2ac-6009-405d-b329-c517bcc5ef67"), false, new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca") },
                    { new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"), 1, new Guid("6f189094-f7e2-4e40-8d8b-c45054be7b96"), false, new Guid("88755139-42b8-415b-84df-04c639d9b47a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"));

            migrationBuilder.DeleteData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"));

            migrationBuilder.DeleteData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88755139-42b8-415b-84df-04c639d9b47a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 7, 10, 43, 136, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 7, 10, 43, 136, DateTimeKind.Local).AddTicks(5083));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 7, 10, 43, 136, DateTimeKind.Local).AddTicks(5088));
        }
    }
}
