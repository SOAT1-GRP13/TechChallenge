using API.Data;
using API.Setup;
using Application.Catalogo.AutoMapper;
using Domain.ValueObjects;
using Infra.Autenticacao;
using Infra.Catalogo;
using Infra.Pagamentos;
using Infra.Pedidos;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(DatabaseSettings.DatabaseConfiguration));
var connectionString = builder.Configuration.GetSection("DatabaseSettings:ConnectionString").Value;

string secret = builder.Configuration.GetSection("ConfiguracaoToken:ClientSecret").Value;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<AutenticacaoContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<CatalogoContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<PedidosContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<PagamentoContext>(options =>
        options.UseNpgsql(connectionString));


builder.Services.Configure<ConfiguracaoToken>(builder.Configuration.GetSection(ConfiguracaoToken.Configuration));
builder.Services.AddAuthenticationJWT(secret);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfig();

builder.Services.AddAutoMapper(typeof(ProdutosMappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.RegisterServices();

// #region [Healthcheck]
// builder.Services.AddHealthChecks()
//             .AddNpgSql(connectionString,
//                 name: "postgreSQL", tags: new string[] { "db", "data" });




// builder.Services.AddHealthChecksUI(opt =>
// {
//     opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
//     opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
//     opt.SetApiMaxActiveRequests(1); //api requests concurrency

//     opt.AddHealthCheckEndpoint("default api", "/health"); //map health check api
// }).AddInMemoryStorage();

// #endregion


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "REDOC API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";
});


// #region [Healthcheck]
// app.UseHealthChecks("/health", new HealthCheckOptions
// {
//     Predicate = _ => true,
//     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

// }).UseHealthChecksUI(h => h.UIPath = "/health-ui");

// #endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await using var scope = app.Services.CreateAsyncScope();
using var dbApplication = scope.ServiceProvider.GetService<ApplicationDbContext>();
using var dbAutenticacao = scope.ServiceProvider.GetService<AutenticacaoContext>();
using var dbCatalogo = scope.ServiceProvider.GetService<CatalogoContext>();
using var dbPagamento = scope.ServiceProvider.GetService<PagamentoContext>();
using var dbPedidos = scope.ServiceProvider.GetService<PedidosContext>();

await dbApplication!.Database.MigrateAsync();
await dbAutenticacao!.Database.MigrateAsync();
await dbCatalogo!.Database.MigrateAsync();
await dbPagamento!.Database.MigrateAsync();
await dbPedidos!.Database.MigrateAsync();

app.Run();
