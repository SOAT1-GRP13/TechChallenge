using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations.Catalogo
{
    public partial class ProdutosDump : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "CategoriaId", "DataCadastro", "Descricao", "Imagem", "Nome", "QuantidadeEstoque", "Valor" },
                values: new object[,]
                {
                    { new Guid("5d98039d-cef1-42d7-970e-f2c256c427a9"), true, new Guid("45dd547f-5209-49c6-8cd1-ee5c384e93f2"), new DateTime(2023, 6, 28, 0, 34, 6, 839, DateTimeKind.Utc).AddTicks(2868), "Hambúrguer de carne com queijo, alface e tomate", "cheeseburger.jpg", "Cheeseburger", 0, 28m },
                    { new Guid("903562cf-1368-4e93-9de3-93f88b1407be"), true, new Guid("90c75224-78fa-4ef1-8b0c-c328781709a4"), new DateTime(2023, 6, 28, 0, 34, 6, 839, DateTimeKind.Utc).AddTicks(2897), "Bebida gaseificada e refrescante", "refrigerante.jpg", "Coca-Cola - Lata", 0, 5m },
                    { new Guid("b06c4c8a-c360-43a5-a786-95a2193b9181"), true, new Guid("9449c867-2f9e-4a3c-8e5f-06d18b788e88"), new DateTime(2023, 6, 28, 0, 34, 6, 839, DateTimeKind.Utc).AddTicks(2891), "Batatas cortadas em palitos e fritas até ficarem crocantes.", "batata_frita.jpg", "Batata Frita", 0, 8m },
                    { new Guid("b5c14afa-c57b-4d9e-b145-400b428c0849"), true, new Guid("8293ecf7-3cab-4a63-99e9-d23d7ea5cd67"), new DateTime(2023, 6, 28, 0, 34, 6, 839, DateTimeKind.Utc).AddTicks(2902), "Sorvete cremoso de baunilha coberto com calda de chocolate e pedaços de chocolate crocantes.", "sundae_chocolate.jpg", "Sundae de Chocolate", 0, 12m }
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
        }
    }
}
