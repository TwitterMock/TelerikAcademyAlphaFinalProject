﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterBackUp.DataModels.Migrations
{
    public partial class SP_DeleteSingleTwitter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SP_DeleteSingleTwitter
                            @TwitterId Varchar(50),
                            @UserId Varchar(50)
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            DELETE FROM UsersTwitters
	                        WHERE UserId = @UserId AND TwitterId = @TwitterId
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
