using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations.Catalogo
{
    public partial class populacategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Codigo", "Nome" },
                values: new object[,]
                {
                    { new Guid("3ec4887d-e6fb-4829-87d8-767d15dfd241"), 2, "Acompanhamento" },
                    { new Guid("5a996f0a-e253-497b-ab62-272c84899de5"), 1, "Lanche" },
                    { new Guid("bfaf52d0-6290-4efe-8587-cc600fdea859"), 3, "Sobremesa" },
                    { new Guid("c2716c94-e1ce-4c56-b3a8-3bbf45e11e2c"), 4, "Bebida" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("3ec4887d-e6fb-4829-87d8-767d15dfd241"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("5a996f0a-e253-497b-ab62-272c84899de5"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("bfaf52d0-6290-4efe-8587-cc600fdea859"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("c2716c94-e1ce-4c56-b3a8-3bbf45e11e2c"));
        }
    }
}
