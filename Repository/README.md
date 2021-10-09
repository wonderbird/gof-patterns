# Repository

In this kata you implement the Gang Of Four Repository Pattern [[1](#ref-1), [2](#ref-2), [3](#ref-3)].

Your team is implementing a sports tracking application. In this context, an exercise represents a unit of physical training. All exercises start at a specific time and date, have a duration in minutes and name the kind of activity, e.g. cycling, swimming, running.

Your task is to create a controller class providing the following operations:

* `ListExercises` returns the list of saved exercises.
* `AddExercise` allows saving an exercise.
* `FindExercisesStartedInTimePeriod` returns the list of saved exercises for which the start time and date lie between two given boundaries.

The controller class shall delegate all interaction with the storage backend to a repository implementation.

Please provide a repository implementation for the following storage backend types:

* in memory - all data shall be kept in memory. The data shall not be persisted. That means that the data is lost after end of the program.
* file - all data shall be saved to and read from a file on disk.
* database - all data shall be saved to and read from a database.

## Prerequisites

* Configure DUnitX by following the DUnitX expert installation instructions in [Stefan Boos: Delphi](https://wonderbird.github.io/pages/software-crafting/programming-languages/delphi.html).

## References

<a name="ref-1">[1]</a> David Starr and others: "Repository" in "Pluralsight: Design Patterns Library"
, https://www.pluralsight.com/courses/patterns-library, last visited on Mar 11, 2020.

<a name="ref-2">[2]</a> Martin Fowler e.a.: Patterns of Enterprise Architecture, [ISBN 978-0321127426](https://isbnsearch.org/isbn/9780321127426).

<a name="ref-3">[3]</a> DevIQ: "Repository Pattern", https://deviq.com/design-patterns/repository-pattern, last visited on Oct. 9, 2021.
