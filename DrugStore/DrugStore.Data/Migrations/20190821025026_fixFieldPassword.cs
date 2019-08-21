using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Data.Migrations
{
    public partial class fixFieldPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 64);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 250);
        }
    }
}
