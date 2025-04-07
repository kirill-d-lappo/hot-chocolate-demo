using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolateDemo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Removed_OrderFood_DirectConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrderItems_Foods_FoodId",
                schema: "dbo",
                table: "FoodOrderItems");

            migrationBuilder.AlterColumn<long>(
                name: "FoodId",
                schema: "dbo",
                table: "FoodOrderItems",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrderItems_Foods_FoodId",
                schema: "dbo",
                table: "FoodOrderItems",
                column: "FoodId",
                principalSchema: "dbo",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodOrderItems_Foods_FoodId",
                schema: "dbo",
                table: "FoodOrderItems");

            migrationBuilder.AlterColumn<long>(
                name: "FoodId",
                schema: "dbo",
                table: "FoodOrderItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodOrderItems_Foods_FoodId",
                schema: "dbo",
                table: "FoodOrderItems",
                column: "FoodId",
                principalSchema: "dbo",
                principalTable: "Foods",
                principalColumn: "Id");
        }
    }
}
