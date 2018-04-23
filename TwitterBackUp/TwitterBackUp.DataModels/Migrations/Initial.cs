using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Twitters",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    FollowersCount = table.Column<int>(nullable: true),
                    FriendsCount = table.Column<int>(nullable: true),
                    ProfileBackgroundImageUrl = table.Column<string>(nullable: true),
                    ProfileImageUrl = table.Column<string>(nullable: true),
                    ScreenName = table.Column<string>(maxLength: 125, nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Username = table.Column<string>(maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Twitters", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    TweetId = table.Column<string>(nullable: false),
                    Content = table.Column<string>(maxLength: 512, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    RetweetCount = table.Column<int>(nullable: true),
                    TwitterAccountId = table.Column<int>(nullable: false),
                    TwitterId = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_Tweets_Twitters_TwitterId",
                        column: x => x.TwitterId,
                        principalTable: "Twitters",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_TwitterId",
                table: "Tweets",
                column: "TwitterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropTable(
                name: "Twitters");
        }
    }
}
