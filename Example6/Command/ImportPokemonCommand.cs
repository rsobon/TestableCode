using Example6.Db;
using Example6.Enums;
using Example6.Reader;
using Example6.Wrappers;
using Microsoft.Extensions.Logging;

namespace Example6.Command;

public class ImportPokemonCommand : IImportPokemonCommand
{
    private readonly ILogger<ImportPokemonCommand> _logger;
    private readonly IPokemonStore _database;
    private readonly IPokemonReader _pokemonReader;
    private readonly IFileSystemWrapper _fileSystemWrapper;

    public ImportPokemonCommand(
        ILogger<ImportPokemonCommand> logger,
        IPokemonStore database,
        IPokemonReader pokemonReader,
        IFileSystemWrapper fileSystemWrapper)
    {
        _logger = logger;
        _database = database;
        _pokemonReader = pokemonReader;
        _fileSystemWrapper = fileSystemWrapper;
    }

    public async Task ImportFiles(string directory)
    {
        var files = _fileSystemWrapper.GetFiles(directory);

        foreach (var filePath in files)
        {
            var status = await ImportPokemon(filePath);
            _logger.LogInformation($"Importing file: \"{filePath}\" finished!. Doing something with a returned status: \"{status}\".");
            if (status == ImportingStatus.Success)
            {
                _fileSystemWrapper.DeleteFile(filePath);
            }
        }
    }

    private async Task<ImportingStatus> ImportPokemon(string filePath)
    {
        try
        {
            _logger.LogInformation($"Received pokemon file to import: {filePath}...");

            var stream = _fileSystemWrapper.OpenRead(filePath);
            var pokemonList = await _pokemonReader.ReadPokemon(stream);

            await _database.SavePokemon(pokemonList);

            _logger.LogInformation($"Saved pokemon from file. Count: {pokemonList.Count}");

            return ImportingStatus.Success;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Error! " + e.Message);
            return ImportingStatus.Error;
        }
    }
}