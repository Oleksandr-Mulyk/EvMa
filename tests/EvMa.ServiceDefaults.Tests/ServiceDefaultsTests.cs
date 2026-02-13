using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.ServiceDiscovery;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;
using System.Net;

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

            var tracerProvider = provider.GetService<TracerProvider>();
            Assert.NotNull(tracerProvider);

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

        [Fact]
        public async Task MapDefaultEndpoints_ReturnsOk_InDevelopment()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();

            builder.Environment.EnvironmentName = Environments.Development;

            builder.WebHost.UseUrls("http://127.0.0.1:0");

            builder.AddServiceDefaults();

            var app = builder.Build();
            app.MapDefaultEndpoints();

            // Act
            await app.StartAsync(TestContext.Current.CancellationToken);

            var address = app.Urls.First();
            using var client = new HttpClient { BaseAddress = new Uri(address) };

            var response = await client.GetAsync("/health", TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await app.StopAsync(TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task MapDefaultEndpoints_ReturnsNotFound_InProduction()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            builder.Environment.EnvironmentName = Environments.Production;

            builder.WebHost.UseUrls("http://127.0.0.1:0");

            builder.AddServiceDefaults();
            var app = builder.Build();
            app.MapDefaultEndpoints();

            await app.StartAsync(TestContext.Current.CancellationToken);

            var address = app.Urls.First();
            using var client = new HttpClient { BaseAddress = new Uri(address) };

            // Act
            var healthResponse = await client.GetAsync("/health", TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, healthResponse.StatusCode);

            await app.StopAsync(TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task MapDefaultApiDocumentation_RedirectsToScalar_InDevelopment()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            builder.Environment.EnvironmentName = Environments.Development;

            builder.Services.AddOpenApi();

            builder.WebHost.UseUrls("http://127.0.0.1:0");
            var app = builder.Build();

            // Act
            app.MapDefaultApiDocumentation();
            await app.StartAsync(TestContext.Current.CancellationToken);

            var address = app.Urls.First();
            using var handler = new HttpClientHandler { AllowAutoRedirect = false };
            using var client = new HttpClient(handler) { BaseAddress = new Uri(address) };

            var response = await client.GetAsync("/", TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/scalar/v1", response.Headers.Location?.OriginalString);

            await app.StopAsync(TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task MapDefaultApiDocumentation_IsDisabled_InProduction()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder();
            builder.Environment.EnvironmentName = Environments.Production;
            builder.Services.AddOpenApi();

            builder.WebHost.UseUrls("http://127.0.0.1:0");
            var app = builder.Build();

            // Act
            app.MapDefaultApiDocumentation();
            await app.StartAsync(TestContext.Current.CancellationToken);

            var address = app.Urls.First();
            using var client = new HttpClient { BaseAddress = new Uri(address) };

            var response = await client.GetAsync("/", TestContext.Current.CancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await app.StopAsync(TestContext.Current.CancellationToken);
        }

        [Fact]
        public void AddDefaultMessaging_RegistersMassTransitWithRabbitMq()
        {
            // Arrange
            var builder = Host.CreateEmptyApplicationBuilder(null);

            var connectionString = "amqp://guest:guest@localhost:5672";
            builder.Configuration["ConnectionStrings:messaging"] = connectionString;

            // Act
            builder.AddDefaultMessaging();
            var provider = builder.Services.BuildServiceProvider();

            // Assert
            var busControl = provider.GetService<IBusControl>();
            Assert.NotNull(busControl);

            var formatter = provider.GetService<IEndpointNameFormatter>();
            Assert.IsType<KebabCaseEndpointNameFormatter>(formatter);

            var busRegistration = provider.GetService<IBusRegistrationContext>();
            Assert.NotNull(busRegistration);
        }
    }
}