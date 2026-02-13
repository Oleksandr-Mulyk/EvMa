using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent);

var customersDb = postgres.AddDatabase("customersdb");

var cache = builder.AddRedis("cache");

var messaging = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin();

builder.Build().Run();
