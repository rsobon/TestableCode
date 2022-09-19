using Example6;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.RegisterServices();
    services.RegisterDbContext(context.Configuration.GetConnectionString("Pokemon"));
    services.AddHostedService<Worker>();
});

var host = builder.Build();
await host.RunAsync();