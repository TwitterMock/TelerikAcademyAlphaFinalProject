using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class UserIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Twitters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TweetId",
                table: "Tweets",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTweets",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TweetId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTweets", x => new { x.UserId, x.TweetId });
                    table.ForeignKey(
                        name: "FK_UsersTweets_Tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTweets_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersTwitters",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TwitterId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTwitters", x => new { x.UserId, x.TwitterId });
                    table.ForeignKey(
                        name: "FK_UsersTwitters_Twitters_TwitterId",
                        column: x => x.TwitterId,
                        principalTable: "Twitters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTwitters_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTweets_TweetId",
                table: "UsersTweets",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTwitters_TwitterId",
                table: "UsersTwitters",
                column: "TwitterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersTweets");

            migrationBuilder.DropTable(
                name: "UsersTwitters");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Twitters",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tweets",
                newName: "TweetId");
        }
    }
}
