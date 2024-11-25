using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolateDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added__UserEntity__ActivityLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ActivityLevel",
                schema: "dbo",
                table: "Users",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ActivityLevel",
                value: (short)0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ActivityLevel",
                value: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                schema: "dbo",
                table: "Users");
        }
    }
}
