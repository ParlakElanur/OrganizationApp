using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organization_Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Role = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Detail = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    ActivityDeadline = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    City = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityID);
                    table.CheckConstraint("CK_DateDeadline", "DATEDIFF(DAY,[ActivityDeadline],[Date])>=1");
                    table.ForeignKey(
                        name: "FK_Activities_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_UserDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    ActivitiesActivityID = table.Column<int>(type: "int", nullable: false),
                    UsersUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUser", x => new { x.ActivitiesActivityID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_ActivityUser_Activities_ActivitiesActivityID",
                        column: x => x.ActivitiesActivityID,
                        principalTable: "Activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUser_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Role" },
                values: new object[] { 1, "admin@gmail.com", "admn", "admn" });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CategoryID",
                table: "Activities",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_UsersUserID",
                table: "ActivityUser",
                column: "UsersUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
