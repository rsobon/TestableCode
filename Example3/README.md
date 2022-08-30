# Example3

## `ImportPokemonCommand`
- Created interfaces
- Wrappers for static and framework dependent classes

## `Program`
- Poor man's dependency injection
- Injected dependencies via constructor (Hollywood principle)

## `PokemonReader`
- Changed `ReadPokemon` to accept `Stream` to be `async`
- `JsonSerializerOptions` initialized in constructor

## `Example3Tests`
- Tests no longer require a physical file.
- Instead mocks can be set up to return what we need.
- We can mock `DateTime.Now`
- Moq, FluentAssertions

