# Design Patterns Katas

[![Build Status Badge](https://github.com/wonderbird/gof-patterns/workflows/.NET/badge.svg)](https://github.com/wonderbird/gof-patterns/actions)

Some of the "Gang of Four" (GoF) design patterns written as kata instructions and associated with a sample implementation for each.

## Status

The project has just been started. The first pattern will be the [Composite](Composite). While implementing that I want to move patterns I have already implemented into this repository.

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=gof-patterns) who provide
an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

## List of GoF Patterns (incomplete)

- [Composite](Composite)

## Development

### Prerequisites

To compile, test and run this project the latest [.NET SDK](https://dotnet.microsoft.com/download) is required on your machine.

### Build, Test, Run

On any computer with the [.NET Core SDK](https://dotnet.microsoft.com/download) run the following commands from the folder containing the `Composite.sln` file in order to build, test and run the application:

```sh
dotnet build
dotnet test
dotnet run --project "Composite.App"
```

### Finishing Touches

- Avoid duplicated code (use `tools\dupfinder.bat`).
- Fix all static code analysis warnings.
- Check the Cyclomatic Complexity of your source code files. For me, the most complex class has a value of (7 - AccuWeather.WindForecastService) and the most complex methods have a value of (3 - GetWindForecastBeaufort in both WindForecastService classes). See Visual Studio -> Analyze -> Calculate Code Metrics.
