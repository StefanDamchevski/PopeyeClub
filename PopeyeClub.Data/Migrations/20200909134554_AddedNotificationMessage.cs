﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PopeyeClub.Data.Migrations
{
    public partial class AddedNotificationMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Notifications",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Notifications");
        }
    }
}
