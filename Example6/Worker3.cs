using Example6.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example6;

public class Worker3 : BackgroundService
{
    private readonly ILogger<Worker3> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public Worker3(
        ILogger<Worker3> logger,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var command = _serviceProvider.GetRequiredService<IImportPokemonCommand>();
            await command.ImportFiles(_configuration.GetValue<string>("Inbox"), stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}