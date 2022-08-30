# Example1

## `ImportPokemonCommand`

- Tight coupling
- Static classes and methods `File`, `DateTime`
- Usage of libraries `JsonConvert`
- Keyword `new`

## `Example1Tests`
- We can only test `ImportingStatus` result

## How to improve testability?

- Class isolation
- Interfaces
- Simple constructors
- Injecting dependencies via new
- Injecting dependencies via factory
- Injecting dependencies via IoC
- Decoupling from global state
- Maintain single responsibility