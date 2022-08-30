# TestableCode
Short demo of designing testable code in .NET Core with interfaces, dependency injection and IoC

Using:
- .NET Core 3.1
- Microsoft Dependency Injection
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
