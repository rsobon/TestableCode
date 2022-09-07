using Example6.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example6;

public class Worker : BackgroundService
{
    #region 1
    // private readonly ILogger<Worker> _logger;
    // private readonly IConfiguration _configuration;
    // private readonly IServiceProvider _serviceProvider;

    // public Worker(
    //     ILogger<Worker> logger,
    //     IConfiguration configuration,
    //     IServiceProvider serviceProvider)
    // {
    //     _logger = logger;
    //     _configuration = configuration;
    //     _serviceProvider = serviceProvider;
    // }
    #endregion

    #region 2
    // private readonly ILogger<Worker> _logger;
    // private readonly IConfiguration _configuration;
    // private readonly IImportPokemonCommand _import;

    // public Worker(
    //     ILogger<Worker> logger,
    //     IConfiguration configuration,
    //     IImportPokemonCommand import)
    // {
    //     _logger = logger;
    //     _configuration = configuration;
    //     _import = import;
    // }
    #endregion

    #region 3
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly Func<IImportPokemonCommand> _importFactory;

    public Worker(
        ILogger<Worker> logger,
        IConfiguration configuration,
        Func<IImportPokemonCommand> importFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _importFactory = importFactory;
    }
    # endregion

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            #region 1
            // var command = _serviceProvider.GetRequiredService<IImportPokemonCommand>();
            // await command.ImportPokemon(_configuration.GetValue<string>("Inbox"));
            #endregion

            #region 2
            // await _import.ImportPokemon(_configuration.GetValue<string>("Inbox"));
            #endregion

            #region 3
            var command = _importFactory();
            await command.ImportPokemon(_configuration.GetValue<string>("Inbox"));
            #endregion

            await Task.Delay(1000, stoppingToken);
        }
    }
}