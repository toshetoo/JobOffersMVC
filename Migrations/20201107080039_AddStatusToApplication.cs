using Microsoft.EntityFrameworkCore.Migrations;

namespace JobOffersMVC.Migrations
{
    public partial class AddStatusToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserApplications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserApplications");
        }
    }
}
