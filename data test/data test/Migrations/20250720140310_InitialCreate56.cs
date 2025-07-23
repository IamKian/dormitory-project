using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_test.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate56 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Dormitories_DormitoryId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Dormitories_Supervisor_DormitoryId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Rooms_RoomId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Person_StudentId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_RoomId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Supervisor_DormitoryId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Supervisor_DormitoryId",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Supervisors");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Supervisors",
                newName: "SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_DormitoryId",
                table: "Supervisors",
                newName: "IX_Supervisors_DormitoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_BlockId",
                table: "Supervisors",
                newName: "IX_Supervisors_BlockId");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supervisors",
                table: "Supervisors",
                column: "SupervisorId");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NationalCode = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    StudentNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DormitoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Dormitories_DormitoryId",
                        column: x => x.DormitoryId,
                        principalTable: "Dormitories",
                        principalColumn: "DormitoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_NationalCode",
                table: "Supervisors",
                column: "NationalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisors_PhoneNumber",
                table: "Supervisors",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DormitoryId",
                table: "Students",
                column: "DormitoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_NationalCode",
                table: "Students",
                column: "NationalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PhoneNumber",
                table: "Students",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoomId",
                table: "Students",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_Blocks_BlockId",
                table: "Supervisors",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisors_Dormitories_DormitoryId",
                table: "Supervisors",
                column: "DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Students_StudentId",
                table: "Tools",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_Blocks_BlockId",
                table: "Supervisors");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisors_Dormitories_DormitoryId",
                table: "Supervisors");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Students_StudentId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supervisors",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisors_NationalCode",
                table: "Supervisors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisors_PhoneNumber",
                table: "Supervisors");

            migrationBuilder.RenameTable(
                name: "Supervisors",
                newName: "Person");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Person",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Supervisors_DormitoryId",
                table: "Person",
                newName: "IX_Person_DormitoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Supervisors_BlockId",
                table: "Person",
                newName: "IX_Person_BlockId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "Person",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "Person",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Supervisor_DormitoryId",
                table: "Person",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_RoomId",
                table: "Person",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Supervisor_DormitoryId",
                table: "Person",
                column: "Supervisor_DormitoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Blocks_BlockId",
                table: "Person",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Dormitories_DormitoryId",
                table: "Person",
                column: "DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Dormitories_Supervisor_DormitoryId",
                table: "Person",
                column: "Supervisor_DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Rooms_RoomId",
                table: "Person",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Person_StudentId",
                table: "Tools",
                column: "StudentId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
