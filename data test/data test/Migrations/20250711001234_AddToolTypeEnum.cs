using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_test.Migrations
{
    /// <inheritdoc />
    public partial class AddToolTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tools",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tools",
                newName: "Name");
        }
    }
}
