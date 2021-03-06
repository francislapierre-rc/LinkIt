# <img src="https://raw.githubusercontent.com/cbcrc/LinkIt/master/logo.png" width="250" title="LinkIt Logo">

LinkIt is an object-oriented data integration library that makes it easy to load different kinds of objects and link them together. 

LinkIt is not an object-relational mapping framework. It can be used for orchestrating the loading of objects and for linking the loaded objects togheter, not for defining how the objects are loaded. LinkIt is intended to be used in a variety of contexts such as data APIs, ETL processes, CQRS event handlers, web crawlers, etc.

### Features
- Minimize coding effort by leveraging reuse and composition
- Data source independent
- Avoid the Select N + 1 problem
- Built-in support for references between complex types
- Support for polymorphism out of the box
- Favor convention over configuration
- Perform complex projections easily with [LinkIt AutoMapper Extensions](https://github.com/cbcrc/LinkIt.AutoMapperExtensions)

### Read more
- [Why Should I Use LinkIt?](docs/why-without-how.md)
- [Getting Started](docs/getting-started.md)
- [Slightly More Complex Example](docs/slightly-more-complex-example.md)
- [Known Limitations](docs/known-limitations.md)
- [License](LICENSE.txt)

### See also
- Perform complex projections easily with [LinkIt AutoMapper Extensions](https://github.com/cbcrc/LinkIt.AutoMapperExtensions)