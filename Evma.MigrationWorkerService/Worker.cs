using EvMa.CatalogService.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace Evma.MigrationWorkerService
{
    public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        : BackgroundService
    {
        public const string ActivitySourceName = "Migrations";
        private static readonly ActivitySource _activitySource = new(ActivitySourceName);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);

            try
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var strategy = dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () => { await dbContext.Database.MigrateAsync(cancellationToken); });
            }
            catch (Exception ex)
            {
                activity?.RecordException(ex);
                throw;
            }

            hostApplicationLifetime.StopApplication();
        }
    }
}
