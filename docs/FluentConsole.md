## FluentConsole

FluentConsole is a wrapper around the `System.Console` class that follows the _Fluent_ pattern, while also adding a number of additional features. All the other tools utilize this wrapper.

#### LogType Enum

|LogType|Notes|
|---|---|
|Info|Applies gray text.|
|Success|Applies green text.|
|Warning|Applies yellow text.|
|Failure|Applies red text.|

#### LogOptions

|Property|DataType|Notes|
|---|---|---|
|IsEndOfLine|Boolean|Determines whether or not a newline character is appended to the message. Defaults to true.|
|ForegroundColor|ConsoleColor|Overrides the foreground color to be used for the log.|
|BackgroundColor|ConsoleColor|Overrides the background color to be used for the log.|

#### Example:

```csharp
new FluentConsole()
    .LineFeed(2)
    .Info("This text is white.")
    .LogSeparator()
    .Success("This text is dark green.")
    .Warning("This text is dark yellow.")
    .Failure("This text is dark red.");
```

Output:
![FluentConsole example](/docs/FluentConsole.png)
