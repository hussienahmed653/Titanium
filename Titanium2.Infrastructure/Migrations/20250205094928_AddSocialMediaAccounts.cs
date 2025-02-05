using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Titanium2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSocialMediaAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    SocialMediaId = table.Column<int>(type: "integer", nullable: false),
                    SocialMediaGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false),
                    Facebook = table.Column<string>(type: "text", nullable: false),
                    Instagram = table.Column<string>(type: "text", nullable: false),
                    Whatsapp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.SocialMediaId);
                    table.ForeignKey(
                        name: "FK_SocialMedias_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_UsersId",
                table: "SocialMedias",
                column: "UsersId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMedias");
        }
    }
}
