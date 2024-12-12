using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolateDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_User_ImageFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                schema: "dbo",
                table: "Users");
        }
    }
}
