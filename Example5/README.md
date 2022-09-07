# Example4

## `PokemonReader`
- Logic in constructor, dependency on `PokemonConfiguration`
- Requires to do more setup for tests
- If dependency in constructor fails, class will fail to instantiate during DI (e.g. if `external-resource.json` fails to deserialize)
- Cannot test the `IsValid` method because it's private

## `PokemonConfiguration`
- Created to simulate accessing external resource to get `AllowedPokemonNames`
- How can this be changed to improve testability?
    - Remove dependency from constructor of `PokemonReader`
    - Call `AllowedPokemonNames()` every time instead of saving it as private field in `PokemonReader`
    - If performance is the issue (and calling `AllowedPokemonNames()` every time) then perhaps use cache and `GetOrAdd` method
    - Services should be stateless if possible
    - `PokemonConfiguration` registered as Singleton
    - New separate `ValidationService` that would be responsible for calling `IsValid` instead of `PokemonReader`

## `Example5_Tests`
- New test `EntityReader_ShouldLogValidationFailed` which uses `ThrowAsync`
- We cannot test `IsValid` method in `PokemonReader`
