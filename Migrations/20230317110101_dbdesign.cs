using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSV_FileReader.Migrations
{
    /// <inheritdoc />
    public partial class dbdesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Months_MonthID",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.RenameColumn(
                name: "MonthID",
                table: "Registrations",
                newName: "TimeID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_MonthID",
                table: "Registrations",
                newName: "IX_Registrations_TimeID");

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Times_TimeID",
                table: "Registrations",
                column: "TimeID",
                principalTable: "Times",
                principalColumn: "TimeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Times_TimeID",
                table: "Registrations");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.RenameColumn(
                name: "TimeID",
                table: "Registrations",
                newName: "MonthID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_TimeID",
                table: "Registrations",
                newName: "IX_Registrations_MonthID");

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    MonthID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.MonthID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Months_MonthID",
                table: "Registrations",
                column: "MonthID",
                principalTable: "Months",
                principalColumn: "MonthID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
