using Microsoft.EntityFrameworkCore.Migrations;

namespace JobOffersMVC.Migrations
{
    public partial class AddForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_CreatorId",
                table: "JobOffers",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Users_CreatorId",
                table: "JobOffers",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Users_CreatorId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_CreatorId",
                table: "JobOffers");
        }
    }
}
