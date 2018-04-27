using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class sfsa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetweetCount",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "TwitterAccountId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tweets");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "Tweets",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetCount",
                table: "Tweets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TwitterAccountId",
                table: "Tweets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tweets",
                nullable: true);
        }
    }
}
