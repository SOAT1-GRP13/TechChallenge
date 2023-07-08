using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations.Autenticacao
{
    public partial class Initial_Acesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "acesso_usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_usuario = table.Column<string>(type: "text", nullable: false),
                    senha = table.Column<string>(type: "text", nullable: false),
                    role_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_acesso_usuario", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "acesso_usuario",
                columns: new[] { "Id", "nome_usuario", "role_usuario", "senha" },
                values: new object[] { new Guid("72dbfdab-ab02-41c3-b4f8-3de33e0faf96"), "fiapUser", 0, "BF16FADFBDF1F8450D143857C08A8ACB70033CC48D18B22037E18A8CA7081F636A0B805E5241E5E30959A21EE773C3293251A34923A8E77925DD140116F25293" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "acesso_usuario");
        }
    }
}
