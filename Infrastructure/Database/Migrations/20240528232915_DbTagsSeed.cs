using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DbTagsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("8c4f32f0-3202-4af4-9532-a89219192219"),
                column: "Title",
                value: "javascript");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("0310ad4d-2719-4418-a90c-f46ffff81cad"), "angular" },
                    { new Guid("03622698-9525-489a-bb52-8eb02f7ee537"), "manual quality assurance" },
                    { new Guid("153f181b-8351-4e76-9369-70c5992dc688"), "reactjs" },
                    { new Guid("20d7d92b-2380-4c0c-9980-b1d995af76db"), "document management" },
                    { new Guid("39fd83eb-97a4-4add-a921-d9fdf22e6c40"), "content management" },
                    { new Guid("602ceb55-7748-48e3-9c4f-b92ccbbd04b2"), ".net" },
                    { new Guid("6b4bea40-57ea-41fc-8754-0197d8a52e9a"), "automation quality assurance" },
                    { new Guid("6d0d2336-d191-4e40-abf8-27d081c464ce"), "creativity" },
                    { new Guid("a65af977-4c65-4665-814c-0106f46059ed"), "ui/ux design" },
                    { new Guid("a9fd5bad-78ee-4b4e-b274-33dc81bdab06"), "artificial intelligence" },
                    { new Guid("afc6d9d0-7c97-4f99-abf0-22a56ab4f454"), "school specialist’s assistance" },
                    { new Guid("c8f290a1-ce07-4429-b0c4-90f6682c0941"), "other" },
                    { new Guid("dbb705b5-20f4-4588-b573-2732c72cb5c7"), "copywriting" },
                    { new Guid("f83c20ff-ec35-4258-97b7-400e18a64e9a"), "languages" }
                });

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 28, 23, 29, 15, 205, DateTimeKind.Utc).AddTicks(2043));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 28, 23, 29, 15, 205, DateTimeKind.Utc).AddTicks(2006));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 28, 23, 29, 15, 205, DateTimeKind.Utc).AddTicks(2040));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("0310ad4d-2719-4418-a90c-f46ffff81cad"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("03622698-9525-489a-bb52-8eb02f7ee537"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("153f181b-8351-4e76-9369-70c5992dc688"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("20d7d92b-2380-4c0c-9980-b1d995af76db"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("39fd83eb-97a4-4add-a921-d9fdf22e6c40"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("602ceb55-7748-48e3-9c4f-b92ccbbd04b2"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("6b4bea40-57ea-41fc-8754-0197d8a52e9a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("6d0d2336-d191-4e40-abf8-27d081c464ce"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a65af977-4c65-4665-814c-0106f46059ed"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a9fd5bad-78ee-4b4e-b274-33dc81bdab06"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("afc6d9d0-7c97-4f99-abf0-22a56ab4f454"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c8f290a1-ce07-4429-b0c4-90f6682c0941"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("dbb705b5-20f4-4588-b573-2732c72cb5c7"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("f83c20ff-ec35-4258-97b7-400e18a64e9a"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("8c4f32f0-3202-4af4-9532-a89219192219"),
                column: "Title",
                value: "js");

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 27, 16, 240, DateTimeKind.Utc).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 27, 16, 240, DateTimeKind.Utc).AddTicks(1299));

            migrationBuilder.UpdateData(
                table: "UserCommunicationChannels",
                keyColumn: "Id",
                keyValue: new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"),
                column: "LastEmailSentAt",
                value: new DateTime(2024, 5, 25, 11, 27, 16, 240, DateTimeKind.Utc).AddTicks(1310));
        }
    }
}
