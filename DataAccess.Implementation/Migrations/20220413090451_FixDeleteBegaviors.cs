using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Implementation.Migrations
{
    public partial class FixDeleteBegaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignTests_AssignedTests_AssignTestId",
                table: "StudentAssignTests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignTests_Participants_StudentId",
                table: "StudentAssignTests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudyGroups_Participants_StudentId",
                table: "StudentStudyGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudyGroups_StudyGroups_StudyGroupId",
                table: "StudentStudyGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignTests_AssignedTests_AssignTestId",
                table: "StudentAssignTests",
                column: "AssignTestId",
                principalTable: "AssignedTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignTests_Participants_StudentId",
                table: "StudentAssignTests",
                column: "StudentId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudyGroups_Participants_StudentId",
                table: "StudentStudyGroups",
                column: "StudentId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudyGroups_StudyGroups_StudyGroupId",
                table: "StudentStudyGroups",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignTests_AssignedTests_AssignTestId",
                table: "StudentAssignTests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignTests_Participants_StudentId",
                table: "StudentAssignTests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudyGroups_Participants_StudentId",
                table: "StudentStudyGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentStudyGroups_StudyGroups_StudyGroupId",
                table: "StudentStudyGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignTests_AssignedTests_AssignTestId",
                table: "StudentAssignTests",
                column: "AssignTestId",
                principalTable: "AssignedTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignTests_Participants_StudentId",
                table: "StudentAssignTests",
                column: "StudentId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudyGroups_Participants_StudentId",
                table: "StudentStudyGroups",
                column: "StudentId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentStudyGroups_StudyGroups_StudyGroupId",
                table: "StudentStudyGroups",
                column: "StudyGroupId",
                principalTable: "StudyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
