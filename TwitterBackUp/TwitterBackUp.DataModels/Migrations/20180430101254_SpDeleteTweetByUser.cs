using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpDeleteTweetByUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE DeleteTweetByUser
                            @TweetId Varchar(50),
                            @UserId Varchar(50)
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            DELETE FROM UsersTweets
	                        WHERE UserId = @UserId AND TweetId = @TweetId
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
