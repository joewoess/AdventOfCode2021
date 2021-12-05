using System.Reflection;

namespace csharp;

public static class Helper
{
    public static bool IsDebug = false;
    public static bool IsTest = false;

    private const string InputPath = "../../input/";
    private const string TestInputPath = "../../testInput/";

    private const string NoSolutionMessage = "NONE";
    private const char SeparatorChar = '-';
    private const string ImplementationNamespace = "csharpimpl.puzzleImpl";

    private const string GreetingMessage = @"AdventOfCode Runner for 2021
Challenge at: https://adventofcode.com/2021/
Author: Johannes Wöß
Written in C# 10 / .NET 6";

    /** Prints a message only if the static variable IsDebug is set */
    public static void DebugMsg(string message)
    {
        if (IsDebug) Console.WriteLine(message);
    }

    /** Prints the beginning header of console output */
    public static void PrintGreeting()
    {
        PrintSeparator();
        Console.WriteLine(GreetingMessage);
        PrintSeparator();
    }

    /** Prints a separator line spanning the console width */
    public static void PrintDay(Type targetType)
    {
        var result = GetPuzzle(targetType);
        Console.WriteLine(
            $"| {targetType.Name} |  {(result?.FirstPuzzle() ?? NoSolutionMessage).PadLeft(10)} |  {(result?.SecondPuzzle() ?? NoSolutionMessage).PadLeft(10)} |");
    }

    /** Prints a header for the results table */
    public static void PrintResultHeader()
    {
        Console.WriteLine($"|  Day  |         1st |         2nd |");
    }

    /** Gets all implementations in the csharpimpl.PuzzleImpl folder */
    public static IEnumerable<Type> GetImplementedTypesFromNamespace()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => string.Equals(t.Namespace, ImplementationNamespace, StringComparison.Ordinal))
            .Where(t => !t.Name.Contains('<') && t.Name.StartsWith("Day"))
            .OrderBy(t => t.Name);
    }

    /** Convert a string of 0s and 1s to a int number */
    public static int BitsToInt(this string stat)
    {
        return Convert.ToInt32(stat, 2);
    }

    // Private methods

    /** Prints a separator line spanning the console width */
    private static void PrintSeparator()
    {
        Console.WriteLine(new string(SeparatorChar, Console.WindowWidth));
    }

    /** Instance the type found by reflection */
    private static IPuzzle? GetPuzzle(Type targetType)
    {
        var filename = Path.Combine(IsTest ? TestInputPath : InputPath, targetType.Name.ToLower() + ".txt");
        DebugMsg($"Reading input from {filename}");
        return (IPuzzle?)Activator.CreateInstance(targetType, filename);
    }
}