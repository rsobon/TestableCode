using Example6.Db;
using Example6.Enums;
using Example6.Reader;
using Example6.Wrappers;
using Microsoft.Extensions.Logging;

namespace Example6.Command;

public class ImportPokemonCommand : IImportPokemonCommand
{
    private readonly ILogger<ImportPokemonCommand> _logger;
    private readonly IPokemonStore _store;
    private readonly IPokemonReader _pokemonReader;
    private readonly IFileSystemWrapper _fileSystemWrapper;

    public ImportPokemonCommand(
        ILogger<ImportPokemonCommand> logger,
        IPokemonStore store,
        IPokemonReader pokemonReader,
        IFileSystemWrapper fileSystemWrapper)
    {
        _logger = logger;
        _store = store;
        _pokemonReader = pokemonReader;
        _fileSystemWrapper = fileSystemWrapper;
    }

    public async Task ImportFiles(string directory, CancellationToken token)
    {
        var files = _fileSystemWrapper.GetFiles(directory);

        foreach (var filePath in files)
        {
            var status = await ImportPokemon(filePath, token);
            _logger.LogInformation($"Importing file: \"{filePath}\" finished!. Status: \"{status}\"");
            if (status == ImportingStatus.Success)
            {
                _logger.LogInformation($"Deleting file: \"{filePath}\"");
                _fileSystemWrapper.DeleteFile(filePath);
            }
        }
    }

    private async Task<ImportingStatus> ImportPokemon(string filePath, CancellationToken token)
    {
        try
        {
            _logger.LogInformation($"Received pokemon file to import: {filePath}...");

            await using var stream = _fileSystemWrapper.OpenRead(filePath);
            var pokemonList = await _pokemonReader.ReadPokemon(stream);
            await _store.SavePokemon(pokemonList, token);
            return ImportingStatus.Success;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Error! " + e.Message);
            return ImportingStatus.Error;
        }
    }
}