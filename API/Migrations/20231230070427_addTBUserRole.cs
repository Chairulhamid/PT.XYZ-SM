using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTBUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_M_UsersAccount_UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropIndex(
                name: "IX_Tb_T_AccountRole_UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropColumn(
                name: "UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.CreateTable(
                name: "Tb_M_UserRole",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_UserRole", x => new { x.Email, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_Tb_M_UserRole_Tb_M_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Tb_M_Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_M_UserRole_Tb_M_UsersAccount_Email",
                        column: x => x.Email,
                        principalTable: "Tb_M_UsersAccount",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_UserRole_Role_Id",
                table: "Tb_M_UserRole",
                column: "Role_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_M_UserRole");

            migrationBuilder.AddColumn<string>(
                name: "UsersAccountEmail",
                table: "Tb_T_AccountRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_AccountRole_UsersAccountEmail",
                table: "Tb_T_AccountRole",
                column: "UsersAccountEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_M_UsersAccount_UsersAccountEmail",
                table: "Tb_T_AccountRole",
                column: "UsersAccountEmail",
                principalTable: "Tb_M_UsersAccount",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
