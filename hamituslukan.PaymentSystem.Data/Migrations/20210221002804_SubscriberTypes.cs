using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hamituslukan.PaymentSystem.Data.Migrations
{
    public partial class SubscriberTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "1ea36e88-91bf-4fc9-855a-65761649525d");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8c639a9c-7b5f-4e41-b837-f5412bd27154", "ce8e975a-0ad5-4049-b5ed-c5280530d241" });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "8c639a9c-7b5f-4e41-b837-f5412bd27154");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ce8e975a-0ad5-4049-b5ed-c5280530d241");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69720b4e-0421-4cde-b608-6a096e068fda", "b4247e47-a37c-4dfb-932c-32f0bebf4433", "Admin", "ADMIN" },
                    { "abdfecc2-9780-4876-a241-a7432dc40966", "571687f2-c16f-472a-a512-8185b33fb4ac", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "SubscriberTypes",
                columns: new[] { "Id", "IdentityLength", "Name" },
                values: new object[,]
                {
                    { new Guid("859a2168-07ed-419d-bd85-8df6f8a1844f"), 11, "Bireysel Müşteri" },
                    { new Guid("62c3030f-7a89-4901-8164-d8cc46b42efe"), 10, "Tüzel Müşteri" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c102ce0f-d00a-4c9d-89bd-1e8322ec7f0d", 0, "63effaf7-f61a-4630-9595-3126d8e5ba5c", "admin@admin.com", false, false, null, "Administrator", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEEufz+b1hrt6ZqcWGeps/0/j3WY6orLOAB4QNjz46hX54MF3q4+sCkqRr2mrKugD3w==", null, false, "39459d23-2bec-47a8-bd66-ba76d65d54ab", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "69720b4e-0421-4cde-b608-6a096e068fda", "c102ce0f-d00a-4c9d-89bd-1e8322ec7f0d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "abdfecc2-9780-4876-a241-a7432dc40966");

            migrationBuilder.DeleteData(
                table: "SubscriberTypes",
                keyColumn: "Id",
                keyValue: new Guid("62c3030f-7a89-4901-8164-d8cc46b42efe"));

            migrationBuilder.DeleteData(
                table: "SubscriberTypes",
                keyColumn: "Id",
                keyValue: new Guid("859a2168-07ed-419d-bd85-8df6f8a1844f"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69720b4e-0421-4cde-b608-6a096e068fda", "c102ce0f-d00a-4c9d-89bd-1e8322ec7f0d" });

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "69720b4e-0421-4cde-b608-6a096e068fda");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "c102ce0f-d00a-4c9d-89bd-1e8322ec7f0d");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8c639a9c-7b5f-4e41-b837-f5412bd27154", "5324e18f-f6ab-4da5-a0e4-71818d31804f", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ea36e88-91bf-4fc9-855a-65761649525d", "c6fdfb8c-785e-4585-ba90-2656e59b3c6a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ce8e975a-0ad5-4049-b5ed-c5280530d241", 0, "aae388ab-d013-40c4-b5e6-a5ef267437be", "admin@admin.com", false, false, null, "Administrator", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEFz2Xtd7ojJ+v2s2z+gb2F+bZo0pPYpG3UWNQTpWxGSa8cbbc9Pz/vGX2X3Wn0PQoA==", null, false, "a8abe568-3679-4c95-9c22-ea9422febbcd", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8c639a9c-7b5f-4e41-b837-f5412bd27154", "ce8e975a-0ad5-4049-b5ed-c5280530d241" });
        }
    }
}
