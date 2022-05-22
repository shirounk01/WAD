using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAD.Migrations
{
    public partial class addedIdentity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights");

            migrationBuilder.DropIndex(
                name: "IX_BookFlights_UserId",
                table: "BookFlights");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookFlights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BookFlights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookFlights_UserId",
                table: "BookFlights",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
