using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class AddCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTweets_Tweets_TweetId",
                table: "UsersTweets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTwitters_Twitters_TwitterId",
                table: "UsersTwitters");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTweets_Tweets_TweetId",
                table: "UsersTweets",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTwitters_Twitters_TwitterId",
                table: "UsersTwitters",
                column: "TwitterId",
                principalTable: "Twitters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersTweets_Tweets_TweetId",
                table: "UsersTweets");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTwitters_Twitters_TwitterId",
                table: "UsersTwitters");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTweets_Tweets_TweetId",
                table: "UsersTweets",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTwitters_Twitters_TwitterId",
                table: "UsersTwitters",
                column: "TwitterId",
                principalTable: "Twitters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
