using API.Data;
using API.Setup;
using Infra.Pedidos;
using Infra.Catalogo;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Application.Pedidos.AutoMapper;
using Swashbuckle.AspNetCore.Filters;
using Application.Catalogo.AutoMapper;
using Domain.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Configuration.AddAmazonSecretsManager("us-west-2", "soat1-grp13");
builder.Services.Configure<Secrets>(builder.Configuration);

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;

string secret = builder.Configuration.GetSection("ClientSecret").Value;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<CatalogoContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddDbContext<PedidosContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddAuthenticationJWT(secret);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfig();

builder.Services.AddAutoMapper(typeof(ProdutosMappingProfile));
builder.Services.AddAutoMapper(typeof(PedidosMappingProfile));
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
using var dbCatalogo = scope.ServiceProvider.GetService<CatalogoContext>();
using var dbPedidos = scope.ServiceProvider.GetService<PedidosContext>();

await dbApplication!.Database.MigrateAsync();
await dbCatalogo!.Database.MigrateAsync();
await dbPedidos!.Database.MigrateAsync();

app.Run();
