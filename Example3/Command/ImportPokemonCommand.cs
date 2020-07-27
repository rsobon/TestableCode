using System;
using System.Threading.Tasks;
using Example3.Db;
using Example3.Enums;
using Example3.Logging;
using Example3.Reader;
using Example3.Wrappers;

namespace Example3.Command
{
    public class ImportPokemonCommand
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

                var fileContent = _fileSystemWrapper.ReadFile(filePath);
                var entity = _pokemonReader.ReadPokemon(fileContent);

                await _database.SavePokemon(entity);
                _logger.Information("Pokemon saved. Id: {pokemon.Id}, Name: {pokemon.Name}, Type: {pokemon.Type}, Timestamp: {pokemon.Timestamp}");

                return ImportingStatus.Success;
            }
            catch (Exception e)
            {
                _logger.Information("Error! " + e.Message);
                return ImportingStatus.Error;
            }
        }
    }
}
