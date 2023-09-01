using Application.Pedidos.Commands;
using Domain.Autenticacao;
using Domain.Catalogo;
using Domain.Pedidos;
using Infra.Autenticacao;
using Infra.Autenticacao.Repository;
using Infra.Catalogo;
using Infra.Catalogo.Repository;
using Infra.Pedidos.Repository;
using Infra.Pedidos;
using MediatR;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using Application.Pedidos.Queries;
using Application.Autenticacao.UseCases;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Handlers;
using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Queries;
using Application.Pedidos.Handlers;
using Application.Pedidos.Boundaries;
using Application.Pedidos.UseCases;
using Application.Pagamentos.Commands;
using Application.Pagamentos.Handlers;
using Application.Catalogo.Queries;
using Application.Catalogo.Commands;
using Application.Catalogo.Boundaries;
using Application.Catalogo.Handlers;

namespace API.Setup
{
    public static class DependencyInjection
    { 
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Domain Notifications 
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Autenticacao
            services.AddTransient<IRequestHandler<AdminAutenticaCommand, LogInUsuarioOutput>, AdminAutenticacaoCommandHandler>();
            services.AddTransient<IRequestHandler<AutenticaClienteCommand, AutenticaClienteOutput>, AutenticaClienteCommandHandler>();
            services.AddTransient<IAutenticacaoRepository, AutenticacaoRepository>();
            services.AddTransient<IRequestHandler<CadastraClienteCommand,AutenticaClienteOutput>,  CadastraClienteCommandHandler>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<IAutenticacaoQuery, AutenticacaoQuery>();
            services.AddScoped<AutenticacaoContext>();

            // Catalogo
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutosQueries, ProdutosQueries>();
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, ProdutoOutput>, AdicionarProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarProdutoCommand, ProdutoOutput>, AtualizarProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverProdutoCommand, bool>, RemoverProdutoCommandHandler>();
            services.AddScoped<CatalogoContext>();

            // Pedidos
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();
            services.AddScoped<IPedidoUseCase, PedidoUseCase>();
            services.AddScoped<PedidosContext>();

            services.AddScoped<IRequestHandler<AtualizarStatusPedidoCommand, PedidoOutput>, AtualizarStatusPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, AdicionarItemPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, AtualizarItemPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, RemoverItemPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, PedidoOutput>, IniciarPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, FinalizarPedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, CancelarProcessamentoPedidoCommandHandler>();
            
            // Pagamento
            services.AddTransient<IRequestHandler<StatusPagamentoCommand, bool>, StatusPagamentoCommandHandler>();

        }
    }
}
