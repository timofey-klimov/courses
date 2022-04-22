using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Implementation.Migrations
{
    public partial class Avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Avatars",
                columns: new[] { "Name", "Content", "CreateDate", "UpdateDate", "UpdatedBy", "CreatedBy" },
                values: new object[,]
                {
                    { "Default",
                        "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MDItMjFUMTE6NTY6MTUrMDE6MDDADLYCAAAAJXRFWHRkYXRlOm1vZGlmeQAyMDE5LTAyLTIxVDExOjU2OjE1KzAxOjAwsVEOvgAAAEZ0RVh0c29mdHdhcmUASW1hZ2VNYWdpY2sgNi43LjgtOSAyMDE5LTAyLTAxIFExNiBodHRwOi8vd3d3LmltYWdlbWFnaWNrLm9yZ0F74sgAAAAYdEVYdFRodW1iOjpEb2N1bWVudDo6UGFnZXMAMaf/uy8AAAAYdEVYdFRodW1iOjpJbWFnZTo6aGVpZ2h0ADUxMsDQUFEAAAAXdEVYdFRodW1iOjpJbWFnZTo6V2lkdGgANTEyHHwD3AAAABl0RVh0VGh1bWI6Ok1pbWV0eXBlAGltYWdlL3BuZz+yVk4AAAAXdEVYdFRodW1iOjpNVGltZQAxNTUwNzQ2NTc1h2wkxQAAABN0RVh0VGh1bWI6OlNpemUAMTAuOEtCQlfZ4lYAAABmdEVYdFRodW1iOjpVUkkAZmlsZTovLy4vdXBsb2Fkcy81Ni81T0RXUFJxLzE4MTIvNDIxMzQ2MC1hY2NvdW50LWF2YXRhci1oZWFkLXBlcnNvbi1wcm9maWxlLXVzZXJfMTE1Mzg2LnBuZ+a6RfcAAAAASUVORK5CYII=",
                        DateTime.Now, 
                        null,
                        null, 
                        1}
                });

            migrationBuilder.Sql("update dbo.Participants set AvatarId = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_AvatarId",
                table: "Participants",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Avatars_AvatarId",
                table: "Participants",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Avatars_AvatarId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Participants_AvatarId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Participants");
        }
    }
}
