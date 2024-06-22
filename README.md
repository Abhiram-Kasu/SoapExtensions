# Extension Methods Library

This is a class library containing various extension methods for common .NET types and tasks.

## Contents

- **EnumerationExtensions** - Extension methods for enumerating over ranges and collections
- **JsonExtensions** - Extension methods for serializing and deserializing JSON
- **TaskExtensions** - Extension methods for chaining and adding timeout to tasks
- **Result Type** - Result type with a Standard error and implicit conversions for cleaner syntax

## Usage

The methods are intended to be used fluently on existing types. For example:
```csharp
using SoapExtensions;

var range = 1..10;

range.ForEach(x => Console.WriteLine(x));

foreach(var i in 1..10)
{
  //Do Something
}

var json = "{ \"name\": \"John\" }";

var obj = json.To<User>(); 

var user = new User();

var json = user.ToJson();


var result = await Task.Run(() => //do some work
                            ).Then(x => //do some more work with the previous result
                            );

Result<int,StandardError> Example(bool condition)
{
    return condition ? 20 : new StandardError("Condition was false");
}

```

See the XML documentation in the source files for specifics on each method. 

## Installation

To use the library in your project:

1. Add the ExtensionMethods project or compiled assembly as a reference
2. Add a `using` directive to include the namespace 
3. Call the extension methods on the desired types

## Contribution

Contributions welcome! Open an issue or PR if you would like to request or add functionality.
