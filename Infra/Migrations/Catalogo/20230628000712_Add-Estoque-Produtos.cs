using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations.Catalogo
{
    public partial class AddEstoqueProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEstoque",
                table: "Produtos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Codigo", "Nome" },
                values: new object[,]
                {
                    { new Guid("45dd547f-5209-49c6-8cd1-ee5c384e93f2"), 1, "Lanche" },
                    { new Guid("8293ecf7-3cab-4a63-99e9-d23d7ea5cd67"), 3, "Sobremesa" },
                    { new Guid("90c75224-78fa-4ef1-8b0c-c328781709a4"), 4, "Bebida" },
                    { new Guid("9449c867-2f9e-4a3c-8e5f-06d18b788e88"), 2, "Acompanhamento" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("45dd547f-5209-49c6-8cd1-ee5c384e93f2"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("8293ecf7-3cab-4a63-99e9-d23d7ea5cd67"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("90c75224-78fa-4ef1-8b0c-c328781709a4"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("9449c867-2f9e-4a3c-8e5f-06d18b788e88"));

            migrationBuilder.DropColumn(
                name: "QuantidadeEstoque",
                table: "Produtos");

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
    }
}
