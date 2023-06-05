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

I am developing during my spare time and use this project for learning purposes. Please assume that I will need some
days to answer your questions.

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

#### Fix Static Code Analysis Warnings

... fix static code analysis warnings reported by [SonarLint](https://www.sonarsource.com/products/sonarlint/)
and by [CodeClimate](https://codeclimate.com/github/wonderbird/TestProcessWrapper/issues).

#### Apply Code Formatting Rules

```shell
# Install https://csharpier.io globally, once
dotnet tool install -g csharpier

# Format code
dotnet csharpier .
```

#### Check Code Metrics

... check code metrics using [metrix++](https://github.com/metrixplusplus/metrixplusplus)

- Configure the location of the cloned metrix++ scripts
  ```shell
  export METRIXPP=/path/to/metrixplusplus
  ```

- Collect metrics
  ```shell
  python "$METRIXPP/metrix++.py" collect --std.code.complexity.cyclomatic --std.code.lines.code --std.code.todo.comments --std.code.maintindex.simple -- .
  ```

- Get an overview
  ```shell
  python "$METRIXPP/metrix++.py" view --db-file=./metrixpp.db
  ```

- Apply thresholds
  ```shell
  python "$METRIXPP/metrix++.py" limit --db-file=./metrixpp.db --max-limit=std.code.complexity:cyclomatic:5 --max-limit=std.code.lines:code:25:function --max-limit=std.code.todo:comments:0 --max-limit=std.code.mi:simple:1
  ```

At the time of writing, I want to stay below the following thresholds:

```text
--max-limit=std.code.complexity:cyclomatic:5
--max-limit=std.code.lines:code:25:function
--max-limit=std.code.todo:comments:0
--max-limit=std.code.mi:simple:1
```

Finally, remove all code duplication. The next section describes how to detect code duplication.

#### Remove Code Duplication Where Appropriate

To detect duplicates I use the [CPD Copy Paste Detector](https://docs.pmd-code.org/latest/pmd_userdocs_cpd.html)
tool from the [PMD Source Code Analyzer Project](https://docs.pmd-code.org/latest/index.html).

If you have installed PMD by download & unzip, replace `pmd` by `./run.sh`.
The [homebrew pmd formula](https://formulae.brew.sh/formula/pmd) makes the `pmd` command globally available.

```sh
# Remove temporary and generated files
# 1. dry run
git clean -ndx
```

```shell
# 2. Remove the files shown by the dry run
git clean -fdx
```

```shell
# Identify duplicated code in files to push to GitHub
pmd cpd --minimum-tokens 50 --language cs --dir .
```
