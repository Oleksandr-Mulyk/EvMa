using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ServiceDiscovery;
using OpenTelemetry.Trace;

namespace EvMa.ServiceDefaults.Tests
{
    public class ServiceDefaultsTests
    {
        [Fact]
        public void AddServiceDefaults_RegistersCoreComponents()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);

            // Act
            builder.AddServiceDefaults();
            var provider = builder.Services.BuildServiceProvider();

            // Assert

            var sdOptions = provider.GetService<IConfigureOptions<ServiceDiscoveryOptions>>();
            Assert.NotNull(sdOptions);

            // 2. ПЕРЕВІРКА OPEN TELEMETRY
            var tracerProvider = provider.GetService<TracerProvider>();
            Assert.NotNull(tracerProvider);

            // 3. ПЕРЕВІРКА HEALTH CHECKS
            var healthOptions = provider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();
            Assert.Contains(healthOptions.Value.Registrations, registration => registration.Name == "self");
        }

        [Fact]
        public void AddServiceDefaults_ConfiguresResilientHttpClient()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);
            builder.AddServiceDefaults();
            var provider = builder.Services.BuildServiceProvider();
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();

            // Act & Assert
            var client = httpClientFactory.CreateClient("test");
            Assert.NotNull(client);
        }
    }
}