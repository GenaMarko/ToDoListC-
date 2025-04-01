using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    categoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    statusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "toDos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    statusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toDos", x => x.id);
                    table.ForeignKey(
                        name: "FK_toDos_category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "category",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_toDos_status_statusId",
                        column: x => x.statusId,
                        principalTable: "status",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "categoryId", "categoryName" },
                values: new object[,]
                {
                    { "call", "Contact" },
                    { "ex", "Exercise" },
                    { "home", "Home" },
                    { "shop", "Shopping" },
                    { "work", "Work" }
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "statusId", "statusName" },
                values: new object[,]
                {
                    { "closed", "Completed" },
                    { "open", "Open" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_toDos_categoryId",
                table: "toDos",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_toDos_statusId",
                table: "toDos",
                column: "statusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "toDos");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
