using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Arcadia.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AdminDataSecurisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d1f3651-74f6-4542-96a1-418e9b9ccb79",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "56130dce-4c0f-4084-81de-8c8e83c81c71", "direction@arcadia-zoo-broceliande.fr", "DIRECTION@ARCADIA-ZOO-BROCELIANDE.FR", "DIRECTION@ARCADIA-ZOO-BROCELIANDE.FR", "AQAAAAIAAYagAAAAEJufGMA9AoIsPs5Zijq6AMnxnAmLmwa/0sy4SCOoNxFtsq+79C/k6SSDDT3mORj6VQ==", "9502e1f1-1cc2-4aed-90eb-ed65e03457a1", "direction@arcadia-zoo-broceliande.fr" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d1f3651-74f6-4542-96a1-418e9b9ccb79",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "21b9d3fd-622a-4a18-a2cd-9ddde5496129", "admin@arcadia.fr", "ADMIN@ARCADIA.FR", "ADMIN@ARCADIA.FR", "AQAAAAIAAYagAAAAED9gu63G1dY1SaqnGuV0oSMis/LOUlL4DRdsbhxudEeeunBbr09GrgZfc2eAuL99AA==", "ae3f4748-94ba-438f-8944-5d02779bdff2", "admin@arcadia.fr" });
        }
    }
}
