using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_test.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Dormitories_SupervisorDormitory_DormitoryId",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "SupervisorDormitory_DormitoryId",
                table: "Person",
                newName: "Supervisor_DormitoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Person",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_SupervisorDormitory_DormitoryId",
                table: "Person",
                newName: "IX_Person_Supervisor_DormitoryId");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Tools",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_StudentId",
                table: "Tools",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Dormitories_Supervisor_DormitoryId",
                table: "Person",
                column: "Supervisor_DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Person_StudentId",
                table: "Tools",
                column: "StudentId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Dormitories_Supervisor_DormitoryId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Person_StudentId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_StudentId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "Supervisor_DormitoryId",
                table: "Person",
                newName: "SupervisorDormitory_DormitoryId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Person",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Person_Supervisor_DormitoryId",
                table: "Person",
                newName: "IX_Person_SupervisorDormitory_DormitoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Dormitories_SupervisorDormitory_DormitoryId",
                table: "Person",
                column: "SupervisorDormitory_DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
