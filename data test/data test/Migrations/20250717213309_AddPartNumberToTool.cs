using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_test.Migrations
{
    /// <inheritdoc />
    public partial class AddPartNumberToTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartNumber",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartNumber",
                table: "Tools");
        }
    }
}
