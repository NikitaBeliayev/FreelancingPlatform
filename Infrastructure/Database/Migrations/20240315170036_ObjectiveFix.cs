using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ObjectiveFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveUser_Users_UsersId",
                table: "ObjectiveUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser");

            migrationBuilder.DropIndex(
                name: "IX_ObjectiveUser_UsersId",
                table: "ObjectiveUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ObjectiveUser",
                newName: "ImplementorsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEmailSentAt",
                table: "UserCommunicationChannels",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Objectives",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser",
                columns: new[] { "ImplementorsId", "ObjectivesId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveUser_ObjectivesId",
                table: "ObjectiveUser",
                column: "ObjectivesId");

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_CreatorId",
                table: "Objectives",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives",
                column: "CreatorId",
                principalTable: "Users",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Users_CreatorId",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ObjectiveUser_Users_ImplementorsId",
                table: "ObjectiveUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser");

            migrationBuilder.DropIndex(
                name: "IX_ObjectiveUser_ObjectivesId",
                table: "ObjectiveUser");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_CreatorId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "ImplementorsId",
                table: "ObjectiveUser",
                newName: "UsersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEmailSentAt",
                table: "UserCommunicationChannels",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveUser",
                table: "ObjectiveUser",
                columns: new[] { "ObjectivesId", "UsersId" });

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 3, 2, 1, 4, 26, 986, DateTimeKind.Local).AddTicks(4558));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 3, 2, 1, 4, 26, 986, DateTimeKind.Local).AddTicks(4608));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 3, 2, 1, 4, 26, 986, DateTimeKind.Local).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 29, 19, 26, 55, 770, DateTimeKind.Utc).AddTicks(5522));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 29, 19, 26, 55, 770, DateTimeKind.Utc).AddTicks(5505));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 2, 29, 19, 26, 55, 770, DateTimeKind.Utc).AddTicks(5519));

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveUser_UsersId",
                table: "ObjectiveUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectiveUser_Users_UsersId",
                table: "ObjectiveUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
