using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmisMessengerApi.Migrations
{
    public partial class file_first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    fileId = table.Column<Guid>(nullable: false),
                    conId = table.Column<string>(nullable: true),
                    fileName = table.Column<string>(nullable: true),
                    filePath = table.Column<string>(nullable: true),
                    fileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.fileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");
        }
    }
}
