﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Employee", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_M_Account_Tb_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    University_id = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_M_Education_Tb_M_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Tb_M_University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.NIK, x.Role_Id });
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Tb_M_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_M_Profilling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_Id = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_M_Profilling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tb_M_Profilling_Tb_M_Account_NIK",
                        column: x => x.NIK,
                        principalTable: "Tb_M_Account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_M_Profilling_Tb_M_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Tb_M_Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_Role_Id",
                table: "AccountRoles",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Education_UniversityId",
                table: "Tb_M_Education",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_M_Profilling_EducationId",
                table: "Tb_M_Profilling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "Tb_M_Profilling");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tb_M_Account");

            migrationBuilder.DropTable(
                name: "Tb_M_Education");

            migrationBuilder.DropTable(
                name: "Tb_M_Employee");

            migrationBuilder.DropTable(
                name: "Tb_M_University");
        }
    }
}
