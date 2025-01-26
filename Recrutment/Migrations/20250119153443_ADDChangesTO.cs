using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrutment.Migrations
{
    /// <inheritdoc />
    public partial class ADDChangesTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "61f3f677-0c8c-452d-aabd-7985308d553a", "e9335a40-75de-4ed3-9a74-fb9a8b48a4ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1b832b62-de6e-4151-ad2d-e79a64b6640d", "30a3b08d-142c-47ea-a629-d1251eba4efe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "36796170-d6e3-4b8f-b120-21197cb8bcd1", "0746ccc9-50fb-422e-b1bd-0d2a549f423f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0da9896c-f8b3-4e46-b890-c24e4c4be6f8", "430962d3-2738-4943-a425-07db281ac0f8" });
        }
    }
}
