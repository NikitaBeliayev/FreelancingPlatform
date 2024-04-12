using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixIsConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { true, new DateTime(2024, 4, 12, 12, 2, 40, 586, DateTimeKind.Utc).AddTicks(6805) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { true, new DateTime(2024, 4, 12, 12, 2, 40, 586, DateTimeKind.Utc).AddTicks(6789) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { true, new DateTime(2024, 4, 12, 12, 2, 40, 586, DateTimeKind.Utc).AddTicks(6802) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { false, new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { false, new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                columns: new[] { "IsConfirmed", "LastEmailSentAt" },
                values: new object[] { false, new DateTime(2024, 4, 4, 21, 24, 24, 141, DateTimeKind.Utc).AddTicks(6161) });
        }
    }
}
