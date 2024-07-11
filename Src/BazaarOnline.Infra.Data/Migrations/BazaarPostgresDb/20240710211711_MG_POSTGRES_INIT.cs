using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations.BazaarPostgresDb
{
    public partial class MG_POSTGRES_INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeatureIntegerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Minimum = table.Column<long>(type: "bigint", nullable: false),
                    Maximum = table.Column<long>(type: "bigint", nullable: false),
                    Placeholder = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureIntegerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureSelectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Options = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureSelectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureStringTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MinLength = table.Column<int>(type: "integer", nullable: false),
                    MaxLength = table.Column<int>(type: "integer", nullable: false),
                    Regex = table.Column<string>(type: "text", nullable: true),
                    Placeholder = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureStringTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileType = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    SizeKB = table.Column<long>(type: "bigint", nullable: false),
                    ExtraProperties = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UsageType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AmarCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsPhoneNumberActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastSeen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: "کاربر بازار"),
                    AnswerHourStart = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    AnswerHourEnd = table.Column<int>(type: "integer", nullable: false, defaultValue: 23)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StringTypeId = table.Column<int>(type: "integer", nullable: true),
                    IntegerTypeId = table.Column<int>(type: "integer", nullable: true),
                    SelectTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_FeatureIntegerTypes_IntegerTypeId",
                        column: x => x.IntegerTypeId,
                        principalTable: "FeatureIntegerTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Features_FeatureSelectTypes_SelectTypeId",
                        column: x => x.SelectTypeId,
                        principalTable: "FeatureSelectTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Features_FeatureStringTypes_StringTypeId",
                        column: x => x.StringTypeId,
                        principalTable: "FeatureStringTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AmarCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blocklist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockerId = table.Column<string>(type: "character varying(36)", nullable: false),
                    BlockedUserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocklist_Users_BlockedUserId",
                        column: x => x.BlockedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blocklist_Users_BlockerId",
                        column: x => x.BlockerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SortNumber = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsFilterable = table.Column<bool>(type: "boolean", nullable: false),
                    IsShownInList = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    FeatureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatures_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Longitude = table.Column<double>(type: "double precision", precision: 3, scale: 7, nullable: true),
                    Latitude = table.Column<double>(type: "double precision", precision: 3, scale: 7, nullable: true),
                    ShowExactCoordinates = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    StatusType = table.Column<string>(type: "text", nullable: false),
                    StatusReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ContactType = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ProvinceId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false),
                    CategoryFeatureId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementFeatures_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementFeatures_CategoryFeatures_CategoryFeatureId",
                        column: x => x.CategoryFeatureId,
                        principalTable: "CategoryFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false),
                    FileCenterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementPictures_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementPictures_FileCenter_FileCenterId",
                        column: x => x.FileCenterId,
                        principalTable: "FileCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<string>(type: "character varying(36)", nullable: false),
                    CustomerId = table.Column<string>(type: "character varying(36)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversation_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversation_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conversation_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAdvertisementBookmark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdvertisementBookmark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdvertisementBookmark_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAdvertisementBookmark_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAdvertisementHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdvertisementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdvertisementHistories_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAdvertisementHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAdvertisementNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    AdvertisementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdvertisementNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdvertisementNotes_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAdvertisementNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeletedConversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedConversations_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeletedConversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, defaultValue: ""),
                    AttachmentJson = table.Column<string>(type: "text", nullable: true),
                    AttachmentType = table.Column<string>(type: "text", nullable: false, defaultValue: "NoAttachment"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsSeen = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsEdited = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReplyToId = table.Column<Guid>(type: "uuid", nullable: true),
                    SenderId = table.Column<string>(type: "character varying(36)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Message_ReplyToId",
                        column: x => x.ReplyToId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeletedMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedMessage_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeletedMessage_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeletedMessage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveCodes_UserId",
                table: "ActiveCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementFeatures_AdvertisementId",
                table: "AdvertisementFeatures",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementFeatures_CategoryFeatureId",
                table: "AdvertisementFeatures",
                column: "CategoryFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementPictures_AdvertisementId",
                table: "AdvertisementPictures",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementPictures_FileCenterId",
                table: "AdvertisementPictures",
                column: "FileCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CategoryId",
                table: "Advertisements",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_CityId",
                table: "Advertisements",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ProvinceId",
                table: "Advertisements",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_UserId",
                table: "Advertisements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocklist_BlockedUserId",
                table: "Blocklist",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocklist_BlockerId",
                table: "Blocklist",
                column: "BlockerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatures_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatures_FeatureId",
                table: "CategoryFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_AmarCode",
                table: "Cities",
                column: "AmarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_AdvertisementId",
                table: "Conversation",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_CustomerId",
                table: "Conversation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_OwnerId",
                table: "Conversation",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedConversations_ConversationId",
                table: "DeletedConversations",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedConversations_UserId",
                table: "DeletedConversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedMessage_ConversationId",
                table: "DeletedMessage",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedMessage_MessageId",
                table: "DeletedMessage",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedMessage_UserId",
                table: "DeletedMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_IntegerTypeId",
                table: "Features",
                column: "IntegerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_SelectTypeId",
                table: "Features",
                column: "SelectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_StringTypeId",
                table: "Features",
                column: "StringTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReplyToId",
                table: "Message",
                column: "ReplyToId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_AmarCode",
                table: "Provinces",
                column: "AmarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementBookmark_AdvertisementId",
                table: "UserAdvertisementBookmark",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementBookmark_UserId",
                table: "UserAdvertisementBookmark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementHistories_AdvertisementId",
                table: "UserAdvertisementHistories",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementHistories_UserId",
                table: "UserAdvertisementHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementNotes_AdvertisementId",
                table: "UserAdvertisementNotes",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdvertisementNotes_UserId",
                table: "UserAdvertisementNotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveCodes");

            migrationBuilder.DropTable(
                name: "AdvertisementFeatures");

            migrationBuilder.DropTable(
                name: "AdvertisementPictures");

            migrationBuilder.DropTable(
                name: "Blocklist");

            migrationBuilder.DropTable(
                name: "DeletedConversations");

            migrationBuilder.DropTable(
                name: "DeletedMessage");

            migrationBuilder.DropTable(
                name: "UserAdvertisementBookmark");

            migrationBuilder.DropTable(
                name: "UserAdvertisementHistories");

            migrationBuilder.DropTable(
                name: "UserAdvertisementNotes");

            migrationBuilder.DropTable(
                name: "CategoryFeatures");

            migrationBuilder.DropTable(
                name: "FileCenter");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "FeatureIntegerTypes");

            migrationBuilder.DropTable(
                name: "FeatureSelectTypes");

            migrationBuilder.DropTable(
                name: "FeatureStringTypes");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
