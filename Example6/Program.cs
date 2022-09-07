using Example6;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

// internal static class Program
// {
//     static async Task Main(string[] args)
//     {
//         var serviceCollection = new ServiceCollection();
//         serviceCollection.RegisterServices();
//         var serviceProvider = serviceCollection.BuildServiceProvider();
//
//         var command = serviceProvider.GetRequiredService<IImportPokemonCommand>();
//         await command.ImportPokemon(@"App_Data\data.json");
//
//         Console.ReadKey();
//     }
// }