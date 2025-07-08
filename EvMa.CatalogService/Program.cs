using EvMa.CatalogService.Data;
using EvMa.CatalogService.Extensions;
using EvMa.CatalogService.Services;
using EvMa.ECommerceLibrary.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRepositories();
builder.Services.AddTransient<ICatalogFactory, DbCatalogFactory>();
builder.Services.AddServiceGrpcModelsToModelsConverters();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("catalogdb"));
});

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProductGrpc>();
app.MapGrpcService<CategoryGrpc>();
app.MapGrpcService<AttributeSetGrpc>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
