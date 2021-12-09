using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmisMessengerApi.Migrations
{
    public partial class addTablePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobpost",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TypeJob = table.Column<int>(nullable: false),
                    RequestSex = table.Column<int>(nullable: false),
                    Experience = table.Column<string>(nullable: true),
                    JobAddress = table.Column<string>(nullable: true),
                    JobDescribe = table.Column<string>(nullable: true),
                    Request = table.Column<string>(nullable: true),
                    Benefit = table.Column<string>(nullable: true),
                    MethodApply = table.Column<string>(nullable: true),
                    Career = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobpost", x => x.PostId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobpost");
        }
    }
}
