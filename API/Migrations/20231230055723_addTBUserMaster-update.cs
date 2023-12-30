using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTBUserMasterupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Corporatebusiness",
                table: "Tb_M_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeCompany",
                table: "Tb_M_User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corporatebusiness",
                table: "Tb_M_User");

            migrationBuilder.DropColumn(
                name: "TypeCompany",
                table: "Tb_M_User");
        }
    }
}
