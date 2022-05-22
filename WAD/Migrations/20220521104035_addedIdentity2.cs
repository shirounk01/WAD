using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WAD.Migrations
{
    public partial class addedIdentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BookFlights",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserGuid",
                table: "BookFlights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "BookFlights");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BookFlights",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookFlights_User_UserId",
                table: "BookFlights",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
