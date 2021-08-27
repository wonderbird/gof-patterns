# Composite

In this kata you implement the Gang Of Four Composite Pattern [[1](#ref-1), [2](#ref-2), [3](#ref-3)].

## Kata Instructions

Your task is to create a method `PrintShoppingCart` to be used by a web shop application. The purpose of the library is to provide the shop with a shopping cart overview for the current customer. The library code will be called when the customer wants to check out the ordered goods.

Hint: In your implementation of the method, no markup is required. Just return plain text.

- `PrintShoppingCart` receives a list of products as argument. This list represents the shopping cart.
- Each product in the cart is characterised by quantity, name and price of the individual item.
- The method shall return a list of strings, one string per product in the shopping cart.
- Each string in the list shall contain the product properties separated by tabulator in the following order: quantity, name, single item price.
- Each string in the list shall end with the total price = quantity * single item price.

Example:

```text
3   Parrot  300.00  900.0
```

After operating the shop for a while, marketing decides to do a campaign. They want to define a set of products for which the custoemr will get a 30 % discount. So the following new requirements are presented to you:

- The shopping cart (list of products) shall allow grouping an arbitrary list of products.
- For each group, `PrintShoppingCart`
  - prints a "group header" starting with an empty quantity field, followed by the group name and the discount in parentheses, an empty individual item price and an empty total price.
  - prints each product as described above whith the 30 % discount applied before printing the price fields

Example:

```text
3   Parrot  300.0   900.0
    Pet Food (Discount: 30 %)
2   Cat Food (12 kg)    40.00   80.00
3   Bird Grass          4.00    12.00
```

In the above case, the customer buys parrots as non-discounted products for 300.00 each and pet food with a discount of 30 % applied. The normal item price of cat food is 60.00 and the normal price of bird grass is 6.00.

## References

<a name="ref-1">[1]</a> John Somnez and others: "Composite" in "Pluralsight: Design Patterns Library", https://www.pluralsight.com/courses/patterns-library, last visited on Aug. 18, 2021.

<a name="ref-2">[2]</a> Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides: "Design Patterns: Elements of Reusable Object-Oriented Software", Addison Wesley, 1994, pp. 151ff, [ISBN 0-201-63361-2](https://en.wikipedia.org/wiki/Special:BookSources/0-201-63361-2).

<a name="ref-3">[3]</a> Wikipedia: "Facade Pattern", https://en.wikipedia.org/wiki/Composite_pattern, last visited on Aug. 18, 2021.
