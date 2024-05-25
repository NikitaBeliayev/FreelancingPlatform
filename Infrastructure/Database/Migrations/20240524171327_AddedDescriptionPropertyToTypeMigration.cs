using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedDescriptionPropertyToTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ObjectiveType",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"),
                column: "Description",
                value: "Longer term or complex initiatives (ex. design and build a full website)");

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"),
                column: "Description",
                value: "Longer term or complex initiatives (ex. design and build a full website)");

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 24, 17, 13, 27, 515, DateTimeKind.Utc).AddTicks(5990));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 24, 17, 13, 27, 515, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 24, 17, 13, 27, 515, DateTimeKind.Utc).AddTicks(5986));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ObjectiveType");

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 22, 7, 42, 12, 891, DateTimeKind.Utc).AddTicks(5792));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 22, 7, 42, 12, 891, DateTimeKind.Utc).AddTicks(5778));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 22, 7, 42, 12, 891, DateTimeKind.Utc).AddTicks(5790));
        }
    }
}
