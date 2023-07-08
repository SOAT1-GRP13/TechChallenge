using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations.Autenticacao
{
    public partial class AcessoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("72dbfdab-ab02-41c3-b4f8-3de33e0faf96"));

            migrationBuilder.CreateTable(
                name: "acesso_cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpf = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acesso_cliente", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "nome_usuario", "role_usuario", "senha" },
                values: new object[] { new Guid("1da79aec-f900-4520-88a3-ee094f636de3"), "fiapUser", 0, "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acesso_cliente");

            migrationBuilder.DeleteData(
                table: "acesso_usuario",
                keyColumn: "Id",
                keyValue: new Guid("1da79aec-f900-4520-88a3-ee094f636de3"));

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "nome_usuario", "role_usuario", "senha" },
                values: new object[] { new Guid("72dbfdab-ab02-41c3-b4f8-3de33e0faf96"), "fiapUser", 0, "BF16FADFBDF1F8450D143857C08A8ACB70033CC48D18B22037E18A8CA7081F636A0B805E5241E5E30959A21EE773C3293251A34923A8E77925DD140116F25293" });
        }
    }
}
