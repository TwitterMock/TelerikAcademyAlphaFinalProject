using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpGetAllTwittersByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp =
                @"CREATE PROCEDURE GetAllTwittersByUserId
                    @UserId varchar(125)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT t.Id, t.Description, t.FollowersCount, t.FriendsCount, t.ProfileBackgroundImageUrl, t.ProfileImageUrl, t.ScreenName, t.Username, t.Url
					FROM Twitters t 
						INNER JOIN UsersTwitters ut ON t.Id = ut.TwitterId
					WHERE @UserId = ut.UserId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
