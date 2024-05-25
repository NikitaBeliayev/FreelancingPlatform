using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixObjectiveTypeDurationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 5, 49, 561, DateTimeKind.Utc).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 5, 49, 561, DateTimeKind.Utc).AddTicks(5549));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 5, 49, 561, DateTimeKind.Utc).AddTicks(5559));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
