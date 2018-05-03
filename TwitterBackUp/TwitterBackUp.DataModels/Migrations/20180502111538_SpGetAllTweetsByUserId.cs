using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpGetAllTweetsByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp =
                @"CREATE PROCEDURE GetAllTweetsByUserId
                    @UserId varchar(125)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT t.Id, t.Content, t.CreatedOn, t.RetweetsCount, t.TwitterScreenName, t.TwitterId, t.Url
					FROM Tweets t 
						INNER JOIN UsersTweets ut ON t.Id = ut.TweetId
					WHERE @UserId = ut.UserId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
