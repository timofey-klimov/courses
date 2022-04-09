using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Implementation.Migrations
{
    public partial class StudyGroupAssignedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyGroupId",
                table: "AssignedTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTests_StudyGroupId",
                table: "AssignedTests",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedTests_StudyGroups_StudyGroupId",
                table: "AssignedTests",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedTests_StudyGroups_StudyGroupId",
                table: "AssignedTests");

            migrationBuilder.DropIndex(
                name: "IX_AssignedTests_StudyGroupId",
                table: "AssignedTests");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "AssignedTests");
        }
    }
}
