# SettingsConfig

Note that SettingsConfig is a temporary, unimprovised name.

Also note that this package is no more than a parser.

SettingsConfig is *yet another storage format* among the likes of JSON and CSV. This one, however, brings a simpler format at the cost of nesting capabilities (which are present, just not as good as JSON's).

<br/>

## Features

- String, Numeric, Boolean, and Tree values (arrays are coming soon)
- Serialization to and from types
- Custom serialization names

<br/>

## Feature Roadmap

- ☐ Arrays
- ☐ Adquate dictionary support
- ☐ Dynamic values/methods
- ☐ Conversion to and from JSON and YAML

<br/>
<br/>

## Table of Contents

- [Quick Start](#quick-start)
- [Examples](#extended-examples)
- [Contribution](#contribution)
- [Root Issues/Known Bugs](#root-issues)

<br/>

## Quick Start


To install you can:

- Download the DLL from your chosen release version
- Download and extract the source code into your codebase
- Download using your preferred package manager (e.g. [nuget](https://www.nuget.org/packages/SettingsConfig/))

There are 2 maintained ways of parsing a SettingsConfig document.
<br/>
The most direct and simplest way is using one of `SettingsDocument`'s creation methods: `SettingsDocument.FromText(System.String)`, `SettingsDocument.FromStream(System.IO.Stream)`, `SettingsDocument.FromParser(SettingsConfig.Parser.SettingsParser)`.

Otherwise, you can directly use the parser (`SettingsParser`) to enumerate raw nodes.

```cs
var parser = new SettingsConfig.Parser.SettingsParser(MyText);
// System.String and System.IO.Stream are both accepted for initialization

foreach (SettingsConfig.Parser.Nodes.SettingsNode node in parser.ParseSyntaxTree())
{
    Console.WriteLine($"Parsed Node: {node.ToFormattedString()}");
}
```

And if you want a document anyway, use `SettingsParser.ParseDocument()`.

## Extended Examples

- [Deserialization](./docs/Examples/Deserialization.cs)

## Contribution

For anyone that actually finds this repository, 
let alone decides to contribute;

- Fork and use pull requests
- Write descriptive issues (use one of the templates if they exist)
- Try to follow the code base's standards when making changes

## Root Issues

A list of known important issues is to be placed here.