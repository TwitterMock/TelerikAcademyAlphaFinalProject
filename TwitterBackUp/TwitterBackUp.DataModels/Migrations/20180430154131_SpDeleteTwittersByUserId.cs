using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SpDeleteTwittersByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SpDeleteTwittersByUserId                        
                            @UserId Varchar(50)
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            DELETE FROM UsersTwitters
	                        WHERE UserId = @UserId 
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
