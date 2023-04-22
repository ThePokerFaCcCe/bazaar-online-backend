using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_USER_StartEndHour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerHourEnd",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 23);

            migrationBuilder.AddColumn<int>(
                name: "AnswerHourStart",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerHourEnd",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AnswerHourStart",
                table: "Users");
        }
    }
}
