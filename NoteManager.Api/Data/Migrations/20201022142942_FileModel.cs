using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteManager.Api.Data.Migrations
{
    public partial class FileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Files",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        "FK_Files_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Notes",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        "FK_Notes_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Files_UserId",
                "Files",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_Notes_UserId",
                "Notes",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Files");

            migrationBuilder.DropTable(
                "Notes");
        }
    }
}