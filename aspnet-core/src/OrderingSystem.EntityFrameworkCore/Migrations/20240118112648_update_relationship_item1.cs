using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingSystem.Migrations
{
    public partial class update_relationship_item1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdsItemCategoryId",
                table: "OdsItem");

            migrationBuilder.DropColumn(
                name: "OdsItemCategoryId",
                table: "OdsCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OdsItemCategoryId",
                table: "OdsItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OdsItemCategoryId",
                table: "OdsCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
