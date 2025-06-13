var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.EvMa_ApiService>("apiservice");

var sql = builder.AddSqlServer("sqlserver");

var catalogDB = sql.AddDatabase("catalogdb");

builder.AddProject<Projects.EvMa_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

var migrationService = builder.AddProject<Projects.Evma_MigrationWorkerService>("evma-migrations")
    .WithReference(catalogDB)
    .WaitFor(catalogDB);

builder.AddProject<Projects.EvMa_CatalogService>("evma-catalogservice")
    .WithReference(catalogDB)
    .WaitFor(catalogDB)
    .WaitFor(migrationService);

builder.Build().Run();
