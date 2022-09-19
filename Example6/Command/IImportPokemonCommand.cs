namespace Example6.Command;

public interface IImportPokemonCommand
{
    Task ImportFiles(string directory, CancellationToken token);
}