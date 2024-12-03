## Confirm

The Confirm tool is used for "yes or no" questions. It supports specifying a default response so that the user can just hit Enter.

### Example:

```csharp
var question = "Are you sure you want to delete all logs?";
var confirm = new Confirm(question, false);
var response = confirm.Run();
```

Output:
![Confirm exmaple](/docs/Confirm.png)
