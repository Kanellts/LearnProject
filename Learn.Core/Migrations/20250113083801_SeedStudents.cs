using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learn.Core.Migrations
{
    public partial class SeedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Name", "Age" },
                values: new object[,]
                {
                { 1, "Jon Smith", 22 },
                { 2, "Veronica Smith", 19 },
                { 3, "Jane Doe", 21 }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 }
            );
        }
    }

}
