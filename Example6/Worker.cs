using Example6.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example6;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory  _serviceScopeFactory;

    public Worker(
        ILogger<Worker> logger,
        IConfiguration configuration,
        IServiceScopeFactory  serviceScopeFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var command = scope.ServiceProvider.GetRequiredService<IImportPokemonCommand>();
                await command.ImportFiles(_configuration.GetValue<string>("Inbox"), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}