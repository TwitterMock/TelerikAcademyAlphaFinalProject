using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class RemoveTweetUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwitterAccountId",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Tweets",
                newName: "TwitterScreenName");

            migrationBuilder.RenameColumn(
                name: "RetweetCount",
                table: "Tweets",
                newName: "RetweetsCount");

            migrationBuilder.AddColumn<string>(
                name: "TwitterId",
                table: "Tweets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwitterId",
                table: "Tweets");

            migrationBuilder.RenameColumn(
                name: "TwitterScreenName",
                table: "Tweets",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "RetweetsCount",
                table: "Tweets",
                newName: "RetweetCount");

            migrationBuilder.AddColumn<int>(
                name: "TwitterAccountId",
                table: "Tweets",
                nullable: false,
                defaultValue: 0);
        }
    }
}
