using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorDeTareas.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class TareasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTerminada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "059247e6-b1ab-46a2-848a-2a8372ab0857", 0, "84bf58ab-ec99-4c65-b544-bdf16fba4a14", "admin@shop.com", true, false, null, "", "ADMIN@SHOP.COM", "ADMIN", "AQAAAAIAAYagAAAAEBeWzcweloFQGm8CspN9KI4zqxibDlFfdthmq4htAJx4zxDpU7egl87pET0lq/4mTg==", null, false, "2d65466b-68f5-4706-93b0-9fa3e25c0d09", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "059247e6-b1ab-46a2-848a-2a8372ab0857");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");
        }
    }
}
