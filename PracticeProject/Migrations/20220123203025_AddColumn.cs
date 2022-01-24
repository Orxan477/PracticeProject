using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeProject.Migrations
{
    public partial class AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Settings",
                nullable: true,defaultValue:false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Settings");
        }
    }
}
