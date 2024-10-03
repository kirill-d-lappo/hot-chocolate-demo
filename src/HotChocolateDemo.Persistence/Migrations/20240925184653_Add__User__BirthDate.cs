using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolateDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add__User__BirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BirthDate",
                schema: "dbo",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "BirthDate",
                value: null);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "BirthDate",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "dbo",
                table: "Users");
        }
    }
}
