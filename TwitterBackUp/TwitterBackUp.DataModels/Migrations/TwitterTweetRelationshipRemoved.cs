using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class TwitterTweetRelationshipRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Twitters_TwitterId",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_TwitterId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TwitterId",
                table: "Tweets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwitterId",
                table: "Tweets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_TwitterId",
                table: "Tweets",
                column: "TwitterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Twitters_TwitterId",
                table: "Tweets",
                column: "TwitterId",
                principalTable: "Twitters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
