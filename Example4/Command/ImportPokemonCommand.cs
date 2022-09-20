using System;
using System.Threading.Tasks;
using Example4.Db;
using Example4.Enums;
using Example4.Logging;
using Example4.Reader;
using Example4.Wrappers;

namespace Example4.Command;

public class ImportPokemonCommand : IImportPokemonCommand
{
    private readonly ILogger _logger;
    private readonly IPokemonStore _database;
    private readonly IPokemonReader _pokemonReader;
    private readonly IFileSystemWrapper _fileSystemWrapper;

    public ImportPokemonCommand(
        ILogger logger,
        IPokemonStore database,
        IPokemonReader pokemonReader,
        IFileSystemWrapper fileSystemWrapper)
    {
        _logger = logger;
        _database = database;
        _pokemonReader = pokemonReader;
        _fileSystemWrapper = fileSystemWrapper;
    }

    public async Task<ImportingStatus> ImportPokemon(string filePath)
    {
        try
        {
            _logger.Information($"Received pokemon to import: {filePath}...");

            var stream = _fileSystemWrapper.OpenRead(filePath);
            var pokemon = await _pokemonReader.ReadPokemon(stream);

            await _database.SavePokemon(pokemon);
            _logger.Information($"Pokemon saved. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");

            return ImportingStatus.Success;
        }
        catch (Exception e)
        {
            _logger.Information("Error! " + e.Message);
            return ImportingStatus.Error;
        }
    }
}