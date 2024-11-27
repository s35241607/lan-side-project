using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lan_side_project.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableToM2M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissions_roles_role_id",
                table: "permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_user_id",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_user_id",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_permissions_role_id",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "permissions");

            migrationBuilder.CreateTable(
                name: "permission_role",
                columns: table => new
                {
                    permissions_id = table.Column<int>(type: "integer", nullable: false),
                    roles_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_role", x => new { x.permissions_id, x.roles_id });
                    table.ForeignKey(
                        name: "FK_permission_role_permissions_permissions_id",
                        column: x => x.permissions_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permission_role_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_user",
                columns: table => new
                {
                    roles_id = table.Column<int>(type: "integer", nullable: false),
                    users_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_user", x => new { x.roles_id, x.users_id });
                    table.ForeignKey(
                        name: "FK_role_user_roles_roles_id",
                        column: x => x.roles_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_user_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permission_role_roles_id",
                table: "permission_role",
                column: "roles_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_user_users_id",
                table: "role_user",
                column: "users_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission_role");

            migrationBuilder.DropTable(
                name: "role_user");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "roles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "permissions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_user_id",
                table: "roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_role_id",
                table: "permissions",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_roles_role_id",
                table: "permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_users_user_id",
                table: "roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
