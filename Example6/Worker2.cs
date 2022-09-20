using Example6.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example6;

public class Worker2 : BackgroundService
{
    private readonly ILogger<Worker2> _logger;
    private readonly IConfiguration _configuration;
    private readonly IImportPokemonCommand _import;

    public Worker2(
        ILogger<Worker2> logger,
        IConfiguration configuration,
        IImportPokemonCommand import)
    {
        _logger = logger;
        _configuration = configuration;
        _import = import;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await _import.ImportFiles(_configuration.GetValue<string>("Inbox"), stoppingToken);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}