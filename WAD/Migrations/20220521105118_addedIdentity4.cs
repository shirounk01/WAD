using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAD.Migrations
{
    public partial class addedIdentity4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookHotels_User_UserId",
                table: "BookHotels");

            migrationBuilder.DropIndex(
                name: "IX_BookHotels_UserId",
                table: "BookHotels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookHotels");

            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "BookHotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "BookHotels");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BookHotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookHotels_UserId",
                table: "BookHotels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookHotels_User_UserId",
                table: "BookHotels",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
