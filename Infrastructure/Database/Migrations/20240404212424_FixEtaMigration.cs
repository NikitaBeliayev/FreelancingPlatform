using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixEtaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eta",
                table: "ObjectiveType");

            migrationBuilder.AddColumn<DateTime>(
                name: "Eta",
                table: "Objectives",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6150));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6161));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eta",
                table: "Objectives");

            migrationBuilder.AddColumn<DateTime>(
                name: "Eta",
                table: "ObjectiveType",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"),
                column: "Eta",
                value: new DateTime(2024, 4, 3, 0, 16, 25, 641, DateTimeKind.Local).AddTicks(5095));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("34719303-dace-07b9-8be3-9a77ee3a48a0"),
                column: "Eta",
                value: new DateTime(2024, 4, 3, 0, 16, 25, 641, DateTimeKind.Local).AddTicks(5142));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"),
                column: "Eta",
                value: new DateTime(2024, 4, 3, 0, 16, 25, 641, DateTimeKind.Local).AddTicks(5147));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4628));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 3, 31, 21, 16, 25, 642, DateTimeKind.Utc).AddTicks(4646));
        }
    }
}
