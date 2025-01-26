using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recrutment.Migrations
{
    /// <inheritdoc />
    public partial class deletepassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Candidats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7f8a4899-410e-4ada-bc0a-287b96d462c2", "46cd657c-5b63-4277-89ab-bc09ff0cc8b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "userId2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bbb8f75f-44f1-44cb-82d5-35c802562781", "8a392cfa-da39-4dfc-ac72-1e1c6f603290" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Candidats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Aymaneelaamaj123");

            migrationBuilder.UpdateData(
                table: "Candidats",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Aymaneelaamaj12334");
        }
    }
}
