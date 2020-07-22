using Microsoft.EntityFrameworkCore.Migrations;

namespace AmisMessengerApi.Migrations
{
    public partial class AddConvid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "convId",
                table: "File",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "convId",
                table: "File");
        }
    }
}
