using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SetIndexUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersTwitters_UserId",
                table: "UsersTwitters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTweets_UserId",
                table: "UsersTweets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersTwitters_UserId",
                table: "UsersTwitters");

            migrationBuilder.DropIndex(
                name: "IX_UsersTweets_UserId",
                table: "UsersTweets");
        }
    }
}
