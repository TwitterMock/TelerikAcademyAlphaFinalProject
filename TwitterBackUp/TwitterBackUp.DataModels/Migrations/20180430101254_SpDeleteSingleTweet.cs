﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpDeleteSingleTweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE DeleteSingleTweet
                            @TweetId Varchar(50),
                            @UserId Varchar(50)
                        AS
                        BEGIN
                            SET NOCOUNT OFF;
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
