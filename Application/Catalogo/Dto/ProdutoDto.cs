namespace Application.Catalogo.Dto
{
    public class ProdutoDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }

    }
}