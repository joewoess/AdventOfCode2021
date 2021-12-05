using static csharp.Helper;

PrintGreeting();
IsTest = args.Contains("--test");
IsDebug = args.Contains("--debug");


var implementations = GetImplementedTypesFromNamespace();
if (args.Contains("--last"))
{
    DebugMsg("Only show last entry (cmd arg --last)");
    PrintSolutionMessage(implementations.Last());
}
else if (args.Length > 0 && int.TryParse(args[0], out var newNum) && newNum is > 0 and <= 25)
{
    var typeFromNumber = implementations.ElementAtOrDefault(newNum - 1);
    DebugMsg($"Only show entry {newNum} (cmd arg NUMBER)");
    if (typeFromNumber is not null) PrintSolutionMessage(typeFromNumber);
}
else
{
    PrintResultHeader();
    foreach (var targetType in implementations)
    {
        DebugMsg(targetType.Name);
        try
        {
            PrintSolutionMessage(targetType);
        }
        catch (Exception e)
        {
            DebugMsg(e.Message);
            Console.WriteLine($"Could not find solution for day {targetType.Name}");
        }
    }
}