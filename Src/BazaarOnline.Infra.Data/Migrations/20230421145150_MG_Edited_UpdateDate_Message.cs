using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_Edited_UpdateDate_Message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Message",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Message",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Message");
        }
    }
}
