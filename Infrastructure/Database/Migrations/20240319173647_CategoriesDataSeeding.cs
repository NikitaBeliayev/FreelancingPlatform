using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class CategoriesDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "c#" },
                    { 2, "java" },
                    { 3, "js" }
                });

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4468));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 3, 21, 20, 36, 47, 29, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7054));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 19, 17, 36, 47, 30, DateTimeKind.Utc).AddTicks(7069));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 3, 17, 19, 0, 35, 7, DateTimeKind.Local).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 3, 17, 19, 0, 35, 7, DateTimeKind.Local).AddTicks(4677));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 3, 17, 19, 0, 35, 7, DateTimeKind.Local).AddTicks(4683));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 15, 17, 0, 35, 8, DateTimeKind.Utc).AddTicks(6457));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 15, 17, 0, 35, 8, DateTimeKind.Utc).AddTicks(6440));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 15, 17, 0, 35, 8, DateTimeKind.Utc).AddTicks(6454));
        }
    }
}
