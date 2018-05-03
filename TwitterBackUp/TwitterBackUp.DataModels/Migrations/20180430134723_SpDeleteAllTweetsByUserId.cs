using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpDeleteAllTweetsByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE DeleteAllTweetsByUserId                        
                            @UserId Varchar(50)
                        AS
                        BEGIN
                            SET NOCOUNT OFF;
                            DELETE FROM UsersTweets
	                        WHERE UserId = @UserId 
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
