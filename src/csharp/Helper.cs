using System.Reflection;
using System.Text;

namespace csharp;

public static class Helper
{
    public static bool IsDebug = false;
    public static bool IsTest = false;

    private const string InputPath = "../../input/";
    private const string TestInputPath = "../../testInput/";

    private const string NoSolutionMessage = "NONE";
    private const char SeparatorChar = '-';
    private const string ImplementationNamespace = "csharp.impl";

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
    public static void PrintSolutionMessage(Type targetType)
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

    /** Gets all implementations in the csharp.impl folder */
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
    
    
    /** Returns an enumerable of points between from and to going preferring to go horizontal then diagonal then vertical */
    public static IEnumerable<Point> Walk(Point from, Point to)
    {
        var points = new List<Point>();
        switch (from)
        {
            case var (x, y) when Math.Abs(x - to.X) == Math.Abs(y - to.Y):
                Range(from.X, to.X)
                    .Zip(Range(from.Y, to.Y))
                    .Select(pos => new Point(pos.First, pos.Second))
                    .ToList()
                    .ForEach(points.Add);
                break;
            case var (x, _) when x == to.X:
                Range(from.Y, to.Y)
                    .Select(y => new Point(x, y))
                    .ToList()
                    .ForEach(points.Add);
                break;
            case var (_, y) when y == to.Y:
                Range(from.X, to.X)
                    .Select(x => new Point(x, y))
                    .ToList()
                    .ForEach(points.Add);
                break;
            default:
                DebugMsg($"Invalid coordinates going from {from} to {to}");
                break;
        }

        return points;
    }

    /** Maps a generic dictionary to a multidimensional grid with a given mapper function */
    public static TGrid[,] PointDictToGrid<TGrid, TMap>(Dictionary<Point, TMap> map, Func<TMap?, TGrid> mapper)
    {
        var minX = map.Keys.Min(p => p.X);
        var maxX = map.Keys.Max(p => p.X);
        var minY = map.Keys.Min(p => p.Y);
        var maxY = map.Keys.Max(p => p.Y);
        var width = maxX - minX + 1;
        var height = maxY - minY + 1;
        var grid = new TGrid[height, width];

        for (var lineIdx = 0; lineIdx < height; lineIdx++)
        {
            for (var posIdx = 0; posIdx < width; posIdx++)
            {
                grid[lineIdx, posIdx] = mapper(map.GetValueOrDefault(new Point(minX + posIdx, minY + lineIdx)));
            }
        }

        return grid;
    }

    /** Converts a generic multidimensional array to a printable string */
    public static string AsPrintable<TGrid>(TGrid[,] grid, Func<TGrid, string>? mapper = null, string? separator = null, int? padLength = null,
        string defaultWithoutMapper = "", string? lineSeparator = "\n")
    {
        var result = new StringBuilder();
        var line = new StringBuilder();

        mapper ??= t => t?.ToString() ?? defaultWithoutMapper;

        for (var lineIdx = 0; lineIdx < grid.GetLength(0); lineIdx++)
        {
            line.Clear();
            for (var posIdx = 0; posIdx < grid.GetLength(1); posIdx++)
            {
                var val = grid[lineIdx, posIdx];
                var mapping = mapper?.Invoke(val) ?? defaultWithoutMapper;
                line.Append(padLength is not null
                    ? mapping.PadLeft(padLength.Value)
                    : mapping);

                if (separator is not null) line.Append(separator);
            }

            result.Append(line);
            if (lineSeparator is not null) result.Append(lineSeparator);
        }

        return result.ToString();
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

    /** Returns an enumerable of ints between from and to counting down if needed */
    private static IEnumerable<int> Range(int from, int to)
    {
        return from > to
            ? Enumerable.Range(to, from - to + 1)
            : Enumerable.Range(from, to - from + 1).Reverse();
    }
}

public readonly record struct Point(int X, int Y)
{
    public override string ToString() => $"({X},{Y})";
}