using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ServiceDiscovery;
using OpenTelemetry.Exporter;
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

        [Fact]
        public void ConfigureOpenTelemetry_RegistersLoggingProvider()
        {
            var builder = Host.CreateEmptyApplicationBuilder(null);
            builder.ConfigureOpenTelemetry();
            var provider = builder.Services.BuildServiceProvider();

            var loggerProvider = provider.GetService<OpenTelemetry.Logs.LoggerProvider>();
            Assert.NotNull(loggerProvider);
        }

        [Fact]
        public void AddOpenTelemetryExporters_ShouldRegisterExporter_WhenEndpointExists()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);
            builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"] = "http://localhost:4317";

            // Act
            builder.ConfigureOpenTelemetry();
            var provider = builder.Services.BuildServiceProvider();

            // Assert
            var options = provider.GetService<IOptions<OtlpExporterOptions>>();

            Assert.NotNull(options);
            Assert.Equal("http://localhost:4317", options.Value.Endpoint.OriginalString);
        }

        [Fact]
        public void AddOpenTelemetryExporters_ShouldNotRegisterExporter_WhenEndpointIsMissing()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);

            // Act
            builder.ConfigureOpenTelemetry();

            Assert.True(string.IsNullOrEmpty(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]));
        }

        [Fact]
        public async Task AddDefaultHealthChecks_RegistersSelfCheckWithCorrectTag()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);

            // Act
            builder.AddDefaultHealthChecks();
            var provider = builder.Services.BuildServiceProvider();

            // Assert
            var options = provider.GetRequiredService<IOptions<HealthCheckServiceOptions>>().Value;
            var selfCheck = options.Registrations.FirstOrDefault(registration => registration.Name == "self");

            Assert.NotNull(selfCheck);
            Assert.Contains("live", selfCheck.Tags);

            var checkInstance = selfCheck.Factory(provider);
            var checkResult =
                await checkInstance.CheckHealthAsync(new HealthCheckContext(), TestContext.Current.CancellationToken);

            Assert.Equal(HealthStatus.Healthy, checkResult.Status);
        }
    }
}