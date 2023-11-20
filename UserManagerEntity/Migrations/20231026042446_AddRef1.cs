using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagerEntity.Migrations
{
    public partial class AddRef1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Blog",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Id",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_Id",
                table: "Blog",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_Id",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_Id",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Blog");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "AspNetUsers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
