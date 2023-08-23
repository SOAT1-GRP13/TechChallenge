using Domain.Base.DomainObjects;
using Domain.Pedidos;


namespace Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Pedido Tests")]

        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            //Act
            pedido.AdicionarItem(pedidoItem);

            //Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Pedido Tests")]

        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(pedidoItem.ProdutoId, "Produto Teste", 1, 100);

            //Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);

            //Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.First(p => p.ProdutoId == pedidoItem.ProdutoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Inexistente")]
        [Trait("Categoria", "Pedido Tests")]

        public void AtualizarItemPedido_ItemNaoExistenteNaLista_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItem));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Pedido Tests")]

        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            //Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            //Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.First(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Deve Calcular Valor Total")]
        [Trait("Categoria", "Pedido Tests")]

        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveCalcularValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto X", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Y", 3, 15);
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Y", 5, 15);
            var totalPedido = pedidoItem.Quantidade * pedidoItem.ValorUnitario +
                               pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            //Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            //Assert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Remover Item que não existe do pedido retorna exception")]
        [Trait("Categoria", "Pedido Tests")]

        public void RemoverItemPedido_ItemNaoExisteNoPedido_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemRemover = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItemRemover));
        }

        [Fact(DisplayName = "Remover Item Pedido Deve Calcular Valor Total")]
        [Trait("Categoria", "Pedido Tests")]

        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem1 = new PedidoItem(produtoId, "Produto X", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Y", 3, 15);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var valorTotalPedido = pedidoItem2.Quantidade * pedidoItem2.ValorUnitario;

            //Act
            pedido.RemoverItem(pedidoItem1);

            //Assert
            Assert.Equal(valorTotalPedido, pedido.ValorTotal);
        }
    }
}
