using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"),
                column: "Password",
                value: "d7b810563cf203ede3043fde799c9705b9ca66635c68cf0465ac3259200f59fe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88755139-42b8-415b-84df-04c639d9b47a"),
                column: "Password",
                value: "afed0e5dd16c3aa13c0913df9557fe7ff05129a2eb4bf9c54b2c68545aec63b1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"),
                column: "Password",
                value: "9259de5682f80ba967a5263d420f44bb40a9267f9787d8034d597a69439e075f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"),
                column: "Password",
                value: "f36711e3bac2be05f53c12f7722279162bd1ecb945a331db5cb1bed89e2d298f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88755139-42b8-415b-84df-04c639d9b47a"),
                column: "Password",
                value: "005ecd48c62dfa2d5d2824958a2ac400fbd82b8e94a8fc5f9fcc9c091a1c7d39");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"),
                column: "Password",
                value: "3c7e2f8d6920bc72da2b24e9d47ca302ce56b6a047ff70d352294bbaf2bf3054");
        }
    }
}
