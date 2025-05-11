using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuInfoManaSys.Migrations
{
    /// <inheritdoc />
    public partial class 添加新列 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Grades");
        }
    }
}
