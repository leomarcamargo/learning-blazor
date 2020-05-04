using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoBlazor.Server.Migrations
{
    public partial class MigrationRoleAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e50e27ce-4792-47fd-87b0-606250021ce0", "8bbe6a8e-4e42-4bf4-8efa-ed64b71da225", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e50e27ce-4792-47fd-87b0-606250021ce0");
        }
    }
}
