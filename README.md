# TestableCode
Short demo of designing testable code in .NET with interfaces, dependency injection and IoC

Using:
- .NET 4.7
- Unity Container
- Moq
- NUnit

## What is testable code?

- Easy to test
- Built from isolated components
- Clean architecture

## Advantages of testable code

- Easy to create unit tests
- Usually easier to create other tests as well (UI, integration etc.)
- Readable code
- Self-documenting (unit tests are good documentation)
- Easy to refactor (interchangeable components)
- Reduce fear of change
- Maintainable code

## Disadvantages of testable code

- Writing code takes more time
- Increased complexity (more abstraction layers)

## Symptoms of not testable code

- “New” keyword (using new on ValueObjects, Dictionaries, Lists is OK).
- Static classes & methods (DateTime, File, Path)
- Framework methods
- Long methods and classes
- Global variables
- Complicated constructors
- Chains of methods (accessing dependencies of dependencies)

## Example 1

```
Data.json -> ImportFileCommand -> Read File -> Deserialize -> Database -> Logger

```

Tight coupling:
- static classes and methods
- framework classes and methods
- new keyword

Conclusion?
- We can only test result of method (what if method is void?)

## How to improve testability?

- Class isolation
- Interfaces
- Simple constructors
- Injecting dependencies via new
- Injecting dependencies via factory
- Injecting dependencies via IoC
- Decoupling from global state
- Maintain single responsibility

## Example 2

- Created new class EntityReader for isolation
- We can test both the Entity and ImportingStatus thanks to extracting logic to isolated class

Conclusion?
- We can't test the Timestamp property of Entity class and Console.WriteLine because they are tightly coupled

## Example 3

- Created interfaces
- Created wrappers for static methods (DateTime, File, Console)
- Injected dependencies via constructor (Hollywood principle)
- We can mock dependencies and create small tests
- NuGet: Moq, Fluent Assertions

Conclusion?
- Poor-man’s injection via new keyword
- Method ImportFileCommand does not have single responsibility

## Example 4

- Used IoC container

Conclusion?
- Watch out for Service Locator temptation
- Deciding what is injectable and newable
- Maintaining single responsibility to keep tests simple and short

## Example 5

- Complicated constructor of `EntityReader`

Conclusion?
- Validation is tightly coupled
- Constructor makes it more difficult to set up test
- Init method is not a solution (services should be stateless if possible)

## Example 6

TODO: Singletons

## Example 7

TODO: Service locators and factories
