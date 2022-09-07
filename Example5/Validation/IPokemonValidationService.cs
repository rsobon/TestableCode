using System.Collections.Generic;

namespace Example5.Validation;

public interface IPokemonValidationService
{
    IList<string> GetAllowedPokemonNames();
}