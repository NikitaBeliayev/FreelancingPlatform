using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ResendEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEmailSentAt",
                table: "UserCommunicationChannels",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 2, 29, 1, 54, 7, 497, DateTimeKind.Local).AddTicks(8722));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 2, 29, 1, 54, 7, 497, DateTimeKind.Local).AddTicks(8785));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 2, 29, 1, 54, 7, 497, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 26, 23, 54, 7, 498, DateTimeKind.Utc).AddTicks(9105));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 26, 23, 54, 7, 498, DateTimeKind.Utc).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 26, 23, 54, 7, 498, DateTimeKind.Utc).AddTicks(9102));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEmailSentAt",
                table: "UserCommunicationChannels");

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 2, 16, 19, 14, 47, 717, DateTimeKind.Local).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 2, 16, 19, 14, 47, 717, DateTimeKind.Local).AddTicks(856));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 2, 16, 19, 14, 47, 717, DateTimeKind.Local).AddTicks(860));
        }
    }
}
