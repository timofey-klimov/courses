using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Implementation.Migrations
{
    public partial class StudetAssignTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTests_Participants_StudentId",
                table: "AssignedTests");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTests_StudentId",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AssignedTests");

            migrationBuilder.CreateTable(
                name: "StudentAssignTests",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssignTestId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignTests", x => new { x.AssignTestId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentAssignTests_AssignedTests_AssignTestId",
                        column: x => x.AssignTestId,
                        principalTable: "AssignedTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignTests_Participants_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignTests_StudentId",
                table: "StudentAssignTests",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssignTests");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "AssignedTests",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AssignedTests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "AssignedTests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "AssignedTests",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "AssignedTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTests_StudentId",
                table: "AssignedTests",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTests_Participants_StudentId",
                table: "AssignedTests",
                column: "StudentId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
