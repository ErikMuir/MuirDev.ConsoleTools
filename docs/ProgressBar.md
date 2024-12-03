## ProgressBar

The ProgressBar tool is designed to provide feedback to the user on the status of long-running processes.

#### ProgressBarConfig

|Property|DataType|Notes|
|---|---|---|
|Label|String|An optional label to appear directly above the progress bar.|
|Display|Boolean|Whether or not to display the progress bar. Defaults to true.|
|DisplayPercentage|Boolean|Whether or not to display the percentage of progress completion. Defaults to false.|
|DisplayBorder|Boolean|Whether or not to display a border around the progress bar. Defaults to false.|
|TextColor|ConsoleColor|The color to use for label and percentage text. Defaults to current foreground color.|
|BorderColor|ConsoleColor|The color to use for the border. Defaults to current foreground color.|
|FillColor|ConsoleColor|The color to use for the progress bar itself. Defaults to current foreground color.|
|Length|int|The length of the progress bar, including border if applicable. Defaults to 80.|

#### Example:

```csharp
var totalSteps = 15;
var config = new ProgressBarConfig
{
    Label = "Processing",
    DisplayBorder = true,
    DisplayPercentage = true,
    TextColor = ConsoleColor.DarkYellow,
    BorderColor = ConsoleColor.DarkCyan,
    FillColor = ConsoleColor.DarkGreen,
};
var status = new ProgressBar(whole, config);
var completedSteps = 0;
var rnd = new Random();

do
{
    status.Update(completedSteps);
    Thread.Sleep(rnd.Next(250)); // simulate actual work
    completedSteps++;
} while (completedSteps <= totalSteps);
```

Output:
![ProgressBar example](/docs/ProgressBar.png)
