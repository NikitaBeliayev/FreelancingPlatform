using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UserObjectiveMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryObjective_Objective_ObjectivesId",
                table: "CategoryObjective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_ObjectiveStatuses_ObjectiveStatusId",
                table: "Objective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_ObjectiveType_TypeId",
                table: "Objective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_Payment_PaymentId",
                table: "Objective");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveStatuses",
                table: "ObjectiveStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Objective",
                table: "Objective");

            migrationBuilder.RenameTable(
                name: "ObjectiveStatuses",
                newName: "ObjectiveStatus");

            migrationBuilder.RenameTable(
                name: "Objective",
                newName: "Objectives");

            migrationBuilder.RenameIndex(
                name: "IX_Objective_TypeId",
                table: "Objectives",
                newName: "IX_Objectives_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Objective_PaymentId",
                table: "Objectives",
                newName: "IX_Objectives_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Objective_ObjectiveStatusId",
                table: "Objectives",
                newName: "IX_Objectives_ObjectiveStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveStatus",
                table: "ObjectiveStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objectives",
                table: "Objectives",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ObjectiveUser",
                columns: table => new
                {
                    ObjectivesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectiveUser", x => new { x.ObjectivesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ObjectiveUser_Objectives_ObjectivesId",
                        column: x => x.ObjectivesId,
                        principalTable: "Objectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObjectiveUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ObjectiveUser_UsersId",
                table: "ObjectiveUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryObjective_Objectives_ObjectivesId",
                table: "CategoryObjective",
                column: "ObjectivesId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_ObjectiveStatus_ObjectiveStatusId",
                table: "Objectives",
                column: "ObjectiveStatusId",
                principalTable: "ObjectiveStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_ObjectiveType_TypeId",
                table: "Objectives",
                column: "TypeId",
                principalTable: "ObjectiveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Payment_PaymentId",
                table: "Objectives",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryObjective_Objectives_ObjectivesId",
                table: "CategoryObjective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_ObjectiveStatus_ObjectiveStatusId",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_ObjectiveType_TypeId",
                table: "Objectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Payment_PaymentId",
                table: "Objectives");

            migrationBuilder.DropTable(
                name: "ObjectiveUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectiveStatus",
                table: "ObjectiveStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Objectives",
                table: "Objectives");

            migrationBuilder.RenameTable(
                name: "ObjectiveStatus",
                newName: "ObjectiveStatuses");

            migrationBuilder.RenameTable(
                name: "Objectives",
                newName: "Objective");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_TypeId",
                table: "Objective",
                newName: "IX_Objective_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_PaymentId",
                table: "Objective",
                newName: "IX_Objective_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_ObjectiveStatusId",
                table: "Objective",
                newName: "IX_Objective_ObjectiveStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectiveStatuses",
                table: "ObjectiveStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objective",
                table: "Objective",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 1, 18, 48, 552, DateTimeKind.Local).AddTicks(831));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 1, 18, 48, 552, DateTimeKind.Local).AddTicks(882));

            migrationBuilder.UpdateData(
                table: "ObjectiveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "Eta",
                value: new DateTime(2024, 2, 7, 1, 18, 48, 552, DateTimeKind.Local).AddTicks(885));

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryObjective_Objective_ObjectivesId",
                table: "CategoryObjective",
                column: "ObjectivesId",
                principalTable: "Objective",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_ObjectiveStatuses_ObjectiveStatusId",
                table: "Objective",
                column: "ObjectiveStatusId",
                principalTable: "ObjectiveStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_ObjectiveType_TypeId",
                table: "Objective",
                column: "TypeId",
                principalTable: "ObjectiveType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_Payment_PaymentId",
                table: "Objective",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
