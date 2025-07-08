var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sql = builder.AddSqlServer("sqlserver")
    .WithDataVolume();

var catalogDB = sql.AddDatabase("catalogdb");

var migrationService = builder.AddProject<Projects.Evma_MigrationWorkerService>("evma-migrations")
    .WithReference(catalogDB)
    .WaitFor(catalogDB);

builder.AddProject<Projects.EvMa_CatalogService>("evma-catalogservice")
    .WithReference(catalogDB)
    .WaitFor(catalogDB);

builder.Build().Run();
