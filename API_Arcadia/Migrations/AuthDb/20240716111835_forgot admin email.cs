using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class forgotadminemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d1f3651-74f6-4542-96a1-418e9b9ccb79",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21b9d3fd-622a-4a18-a2cd-9ddde5496129", "admin@arcadia.fr", "AQAAAAIAAYagAAAAED9gu63G1dY1SaqnGuV0oSMis/LOUlL4DRdsbhxudEeeunBbr09GrgZfc2eAuL99AA==", "ae3f4748-94ba-438f-8944-5d02779bdff2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d1f3651-74f6-4542-96a1-418e9b9ccb79",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7592232b-aec0-40e3-b00b-f2e39b27a532", null, "AQAAAAIAAYagAAAAEP8bHHbP+ZoTZVI7bha8drXJXkYmHplJBem+pmA4ex+4EJyd3PvojttMovn6HEatCQ==", "9f0fd5b3-4bf7-42b2-823e-a5dd54413b91" });
        }
    }
}
