using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, configurationBuilder) =>
{
    configurationBuilder.AddJsonFile($"ocelot.{context.HostingEnvironment.EnvironmentName}.json");
});

builder.Logging
    .AddConfiguration(builder.Configuration.GetSection("Logging"))
    .AddConsole()
    .AddDebug();

builder.Services
    .AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

await app.UseOcelot();
app.MapGet("/", () => "Hello World!");

app.Run();