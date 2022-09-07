using System.Text.Json;
using Example6.Enums;

namespace Example6.Validation;

public class PokemonValidationService : IPokemonValidationService
{
    public IList<PokemonType> GetAllowedPokemonTypes()
    {
        var list = new List<PokemonType>();
        var resourceJson = File.ReadAllText(@"App_Data\external-resource.json");
        var dto = JsonSerializer.Deserialize<PokemonTypeDto>(resourceJson);

        if (dto == null)
        {
            return list;
        }

        foreach (var type in dto.AllowedPokemonTypes)
        {
            if (Enum.TryParse<PokemonType>(type, out var result))
            {
                list.Add(result);
            }
        }

        return list;
    }
}

internal class PokemonTypeDto
{
    public IList<string> AllowedPokemonTypes { get; set; } = new List<string>();
}