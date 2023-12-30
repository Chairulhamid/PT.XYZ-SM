using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTBUserMasterupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_User",
                table: "Tb_M_User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tb_M_User");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Tb_M_User");

            migrationBuilder.AddColumn<string>(
                name: "UsersAccountEmail",
                table: "Tb_T_AccountRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Tb_M_User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_User",
                table: "Tb_M_User",
                column: "Email");

            migrationBuilder.CreateTable(
                name: "Tb_M_UsersAccount",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_UsersAccount", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Tb_M_UsersAccount_Tb_M_User_Email",
                        column: x => x.Email,
                        principalTable: "Tb_M_User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tb_T_AccountRole_Tb_M_UsersAccount_UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropTable(
                name: "Tb_M_UsersAccount");

            migrationBuilder.DropIndex(
                name: "IX_Tb_T_AccountRole_UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tb_M_User",
                table: "Tb_M_User");

            migrationBuilder.DropColumn(
                name: "UsersAccountEmail",
                table: "Tb_T_AccountRole");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Tb_M_User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tb_M_User",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Tb_M_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tb_M_User",
                table: "Tb_M_User",
                column: "Id");
        }
    }
}
