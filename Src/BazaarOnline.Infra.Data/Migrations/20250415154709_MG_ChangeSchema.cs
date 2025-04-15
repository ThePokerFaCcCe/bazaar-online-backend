using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_ChangeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bazaar");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementNotes",
                newName: "UserAdvertisementNotes",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementHistories",
                newName: "UserAdvertisementHistories",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementBookmark",
                newName: "UserAdvertisementBookmark",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Provinces",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Message",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "FileCenter",
                newName: "FileCenter",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "FeatureStringTypes",
                newName: "FeatureStringTypes",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "FeatureSelectTypes",
                newName: "FeatureSelectTypes",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Features",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "FeatureIntegerTypes",
                newName: "FeatureIntegerTypes",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "DeletedMessage",
                newName: "DeletedMessage",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "DeletedConversations",
                newName: "DeletedConversations",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversation",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "Cities",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "CategoryFeatures",
                newName: "CategoryFeatures",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Blocklist",
                newName: "Blocklist",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                newName: "Advertisements",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "AdvertisementPictures",
                newName: "AdvertisementPictures",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "AdvertisementFeatures",
                newName: "AdvertisementFeatures",
                newSchema: "bazaar");

            migrationBuilder.RenameTable(
                name: "ActiveCodes",
                newName: "ActiveCodes",
                newSchema: "bazaar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "bazaar",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementNotes",
                schema: "bazaar",
                newName: "UserAdvertisementNotes");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementHistories",
                schema: "bazaar",
                newName: "UserAdvertisementHistories");

            migrationBuilder.RenameTable(
                name: "UserAdvertisementBookmark",
                schema: "bazaar",
                newName: "UserAdvertisementBookmark");

            migrationBuilder.RenameTable(
                name: "Provinces",
                schema: "bazaar",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "Message",
                schema: "bazaar",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "FileCenter",
                schema: "bazaar",
                newName: "FileCenter");

            migrationBuilder.RenameTable(
                name: "FeatureStringTypes",
                schema: "bazaar",
                newName: "FeatureStringTypes");

            migrationBuilder.RenameTable(
                name: "FeatureSelectTypes",
                schema: "bazaar",
                newName: "FeatureSelectTypes");

            migrationBuilder.RenameTable(
                name: "Features",
                schema: "bazaar",
                newName: "Features");

            migrationBuilder.RenameTable(
                name: "FeatureIntegerTypes",
                schema: "bazaar",
                newName: "FeatureIntegerTypes");

            migrationBuilder.RenameTable(
                name: "DeletedMessage",
                schema: "bazaar",
                newName: "DeletedMessage");

            migrationBuilder.RenameTable(
                name: "DeletedConversations",
                schema: "bazaar",
                newName: "DeletedConversations");

            migrationBuilder.RenameTable(
                name: "Conversation",
                schema: "bazaar",
                newName: "Conversation");

            migrationBuilder.RenameTable(
                name: "Cities",
                schema: "bazaar",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "CategoryFeatures",
                schema: "bazaar",
                newName: "CategoryFeatures");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "bazaar",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Blocklist",
                schema: "bazaar",
                newName: "Blocklist");

            migrationBuilder.RenameTable(
                name: "Advertisements",
                schema: "bazaar",
                newName: "Advertisements");

            migrationBuilder.RenameTable(
                name: "AdvertisementPictures",
                schema: "bazaar",
                newName: "AdvertisementPictures");

            migrationBuilder.RenameTable(
                name: "AdvertisementFeatures",
                schema: "bazaar",
                newName: "AdvertisementFeatures");

            migrationBuilder.RenameTable(
                name: "ActiveCodes",
                schema: "bazaar",
                newName: "ActiveCodes");
        }
    }
}
