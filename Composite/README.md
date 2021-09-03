# Composite

In this kata you implement the Gang Of Four Composite Pattern [[1](#ref-1), [2](#ref-2), [3](#ref-3)].

## Kata Instructions

- Create a program maintaining a list of squares.

- Create a class `Rectangle` with the following attributes:
  - `Width` specifies the width of the rectangle
  - `Height` specifies the height of the rectangle
  - `Left` specifies the left x coordinate of the square
  - `Top` specifies the top y coordinate of the square

- For each shape the program shall print the bounding box, i.e. the top left and bottom right coordinate. Example: For a rectangle with `Width = 3, Height = 5, Left = 1, Top = 2` the program shall print `(1,2), (4, 7)`

- Create a class `Circle` with the following attributes:
  - `Radius`
  - `CenterX` specifies the x coordinate of the center of the circle
  - `CenterY` specifies the y coordinate of the center of the circle

- The program shall allow mixing squares and circles in the list it maintains.

- The program shall print the correct bounding box for each shape.

- Create a class `Intersection` which can hold an arbitrary number of circles and rectangles.

- The program shall allow mixing `Intersection` objects with the other shapes and print the correct bounding boxes.

- For an `Intersection` object, the bounding box is calculated such, that all objects within the intersection are covered by the bounding box.

## References

<a name="ref-1">[1]</a> John Somnez and others: "Composite" in "Pluralsight: Design Patterns Library"
, https://www.pluralsight.com/courses/patterns-library, last visited on Aug. 18, 2021.

<a name="ref-2">[2]</a> Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides: "Design Patterns: Elements of Reusable
Object-Oriented Software", Addison Wesley, 1994, pp.
151ff, [ISBN 0-201-63361-2](https://en.wikipedia.org/wiki/Special:BookSources/0-201-63361-2).

<a name="ref-3">[3]</a> Wikipedia: "Facade Pattern", https://en.wikipedia.org/wiki/Composite_pattern, last visited on
Aug. 18, 2021.