using Example6.Enums;

namespace Example6.Validation;

public interface IPokemonValidationService
{
    IList<PokemonType> GetAllowedPokemonTypes();
}