using Example6;
using Example6.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
    {
        services.RegisterServices();
        services.AddDbContext<PokemonDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("Pokemon")));
        services.AddHostedService<Worker>();
    })
    .Build();

var host = builder.Build();
await host.RunAsync();