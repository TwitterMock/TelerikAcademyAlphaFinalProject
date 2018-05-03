using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class AddRetweetAndFollowColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFollowed",
                table: "UsersTwitters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRetweeted",
                table: "UsersTweets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tweets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFollowed",
                table: "UsersTwitters");

            migrationBuilder.DropColumn(
                name: "IsRetweeted",
                table: "UsersTweets");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tweets");
        }
    }
}
