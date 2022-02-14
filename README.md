# Design Patterns Katas

[![Build Status Badge](https://github.com/wonderbird/gof-patterns/workflows/.NET/badge.svg)](https://github.com/wonderbird/gof-patterns/actions/workflows/dotnet.yml)
[![Test Coverage](https://img.shields.io/codeclimate/coverage-letter/wonderbird/gof-patterns)](https://codeclimate.com/github/wonderbird/gof-patterns/trends/test_coverage_total)
[![Code Maintainability](https://img.shields.io/codeclimate/maintainability-percentage/wonderbird/gof-patterns)](https://codeclimate.com/github/wonderbird/gof-patterns)
[![Issues in Code](https://img.shields.io/codeclimate/issues/wonderbird/gof-patterns)](https://codeclimate.com/github/wonderbird/gof-patterns/issues)
[![Technical Debt](https://img.shields.io/codeclimate/tech-debt/wonderbird/gof-patterns)](https://codeclimate.com/github/wonderbird/gof-patterns)
[![CodeQL](https://github.com/wonderbird/gof-patterns/workflows/CodeQL/badge.svg)](https://github.com/wonderbird/gof-patterns/actions/workflows/codeql-analysis.yml)

Some of the "Gang of Four" (GoF) design patterns written as kata instructions and associated with a sample
implementation for each.

## Status

The project has been started quite recently. Some patterns are already available and I am working on the [Repository](Repository) at the moment. While implementing that I want
to move patterns I have already implemented into this repository.

Note, that I just learned that there are existing GitHub repositories with the same project goal in mind as this one:
- [GitHub: Refactoring.Guru](https://github.com/RefactoringGuru) - several repositories starting with `design-patterns-` are available.
- [Jim McKeeth: DelphiPatterns](https://github.com/jimmckeeth/DelphiPatterns), mirrored by [Refactoring.Guru: design-patterns-delphi](https://github.com/RefactoringGuru/design-patterns-delphi)

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=gof-patterns) who provide
an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

Many thanks to [Embarcadero](https://www.embarcadero.com/) who provide
an [Open Source License](https://www.embarcadero.com/products/delphi/starter) to the public ❤️.

## List of Implemented GoF Patterns

- [Bridge](Bridge)
- [Composite](Composite)
- [Facade](Facade)
- [Repository](Repository) (Delphi)

## Development

### Prerequisites

To compile, test and run this project the latest [.NET SDK](https://dotnet.microsoft.com/download) is required on your
machine.

### Build, Test, Run

On any computer with the [.NET SDK](https://dotnet.microsoft.com/download) run the following commands from the folder
containing the `GoFPatterns.sln` file in order to build and test:

```shell
dotnet build
dotnet test
```

To run one of the contained applications, e.g. the wind forecast app, enter:

```shell
dotnet run --project "Facade/Facade.App"
```

### Before Creating a Pull Request ...

#### ... apply code formatting rules

```shell
dotnet format
```

#### ... check code metrics

Use [metrix++](https://github.com/metrixplusplus/metrixplusplus) to apply thresholds:

```shell
# Collect metrics
metrix++ collect --std.code.complexity.cyclomatic --std.code.lines.code --std.code.todo.comments --std.code.maintindex.simple -- .

# Get an overview
metrix++ view --db-file=./metrixpp.db

# Apply thresholds
metrix++ limit --db-file=./metrixpp.db --max-limit=std.code.complexity:cyclomatic:5 --max-limit=std.code.lines:code:25:function --max-limit=std.code.todo:comments:0 --max-limit=std.code.mi:simple:1
```

At the time of writing, I want to stay below the following thresholds:

```shell
--max-limit=std.code.complexity:cyclomatic:5
--max-limit=std.code.lines:code:25:function
--max-limit=std.code.todo:comments:0
--max-limit=std.code.mi:simple:1
```

### ... fix code duplication

The `tools\dupfinder.bat` or `tools/dupfinder.sh` file calls
the [JetBrains dupfinder](https://www.jetbrains.com/help/resharper/dupFinder.html) tool and creates an HTML report of
duplicated code blocks in the solution directory.

In order to use the `dupfinder` you need to globally install
the [JetBrains ReSharper Command Line Tools](https://www.jetbrains.com/help/resharper/ReSharper_Command_Line_Tools.html)
On Unix like operating systems you also need [xsltproc](http://xmlsoft.org/XSLT/xsltproc2.html), which is pre-installed
on macOS.

From the folder containing the `.sln` file run

```sh
tools\dupfinder.bat
```

or

```sh
tools/dupfinder.sh
```

respectively.

The report will be created as `dupfinder-report.html` in the current directory.