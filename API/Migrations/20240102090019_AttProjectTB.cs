using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AttProjectTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_T_Profiling");

            migrationBuilder.DropTable(
                name: "Tb_M_Education");

            migrationBuilder.DropTable(
                name: "Tb_T_University");

            migrationBuilder.CreateTable(
                name: "Tb_M_Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_M_Project_Tb_M_User_Email",
                        column: x => x.Email,
                        principalTable: "Tb_M_User",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Project_Email",
                table: "Tb_M_Project",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_M_Project");

            migrationBuilder.CreateTable(
                name: "Tb_T_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: true),
                    University_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_M_Education_Tb_T_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Tb_T_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tb_T_Profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: true),
                    Education_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_T_Profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_Tb_M_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Tb_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_T_Profiling_Tb_T_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_T_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Education_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_T_Profiling_EducationId",
                table: "Tb_T_Profiling",
                column: "EducationId");
        }
    }
}
