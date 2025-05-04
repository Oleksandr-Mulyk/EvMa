var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.EvMa_ApiService>("apiservice");

builder.AddProject<Projects.EvMa_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.EvMa_CatalogService>("evma-catalogservice");

builder.Build().Run();
