using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_DeletedMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "کاربر بازار",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldDefaultValue: "کاربر بازار");

            migrationBuilder.CreateTable(
                name: "DeletedMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeletedMessage");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "کاربر بازار",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldDefaultValue:  "کاربر بازار");
        }
    }
}
