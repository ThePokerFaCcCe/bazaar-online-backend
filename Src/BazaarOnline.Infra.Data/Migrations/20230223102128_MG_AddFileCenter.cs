using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_AddFileCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureName",
                table: "AdvertisementPictures");

            migrationBuilder.AddColumn<int>(
                name: "FileCenterId",
                table: "AdvertisementPictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FileCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SizeKB = table.Column<long>(type: "bigint", nullable: false),
                    UsageType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCenter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementPictures_FileCenterId",
                table: "AdvertisementPictures",
                column: "FileCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementPictures_FileCenter_FileCenterId",
                table: "AdvertisementPictures",
                column: "FileCenterId",
                principalTable: "FileCenter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementPictures_FileCenter_FileCenterId",
                table: "AdvertisementPictures");

            migrationBuilder.DropTable(
                name: "FileCenter");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementPictures_FileCenterId",
                table: "AdvertisementPictures");

            migrationBuilder.DropColumn(
                name: "FileCenterId",
                table: "AdvertisementPictures");

            migrationBuilder.AddColumn<string>(
                name: "PictureName",
                table: "AdvertisementPictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
