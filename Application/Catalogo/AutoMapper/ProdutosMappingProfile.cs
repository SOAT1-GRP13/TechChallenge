
using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;
using AutoMapper;
using Domain.Catalogo;

namespace Application.Catalogo.AutoMapper
{
    public class ProdutosMappingProfile : Profile
    {
        public ProdutosMappingProfile()
        {
            CreateMap<Produto, ProdutoDto>();

            CreateMap<Categoria, CategoriaDto>();

            CreateMap<ProdutoDto, Produto>()
                .ConstructUsing(p =>
                    new Produto(p.Nome, p.Descricao, p.Ativo,
                        p.Valor, p.CategoriaId, p.DataCadastro,
                        p.Imagem));

            CreateMap<CategoriaDto, Categoria>()
                .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));


            CreateMap<Produto, ProdutoOutput>();
            CreateMap<ProdutoInput, Produto>();

            CreateMap<ProdutoEditarInput, Produto>();
            
        }
    }
}