using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lan_side_project.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_password_changed_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_password_reset_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "login_failed_attempts",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "login_lockout_end",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "reset_password_failed_attempts",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_password_lockout_end",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reset_password_token",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reset_password_token_expiration",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_password_changed_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_password_reset_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "login_failed_attempts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "login_lockout_end",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_password_failed_attempts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_password_lockout_end",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_password_token",
                table: "users");

            migrationBuilder.DropColumn(
                name: "reset_password_token_expiration",
                table: "users");
        }
    }
}
