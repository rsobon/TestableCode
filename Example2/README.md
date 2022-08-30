# Example2

## `ImportPokemonCommand`

- Created new class `PokemonReader` for isolation
- Less dependencies in `ImportPokemonCommand`
- Still tightly coupled because of `new` keywords

## `PokemonReader`
- Easy to swap library/dependency like `System.Text.Json` instead of `Newtonsoft`

## `Example2Tests`
- We can test both the `Pokemon` entity and `ImportingStatus` thanks to extracting logic to isolated class
- We can't however test the `Timestamp` property of Pokemon class and `Console.WriteLine` because it's tightly coupled