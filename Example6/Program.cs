using Example6;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.RegisterServices();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();