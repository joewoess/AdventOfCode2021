using static csharp.Helper;

namespace csharp.puzzleImpl;

public class Day02 : IPuzzle
{
    private readonly string[] _input;

    public Day02(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var depth = 0;
        var position = 0;

        var commands = _input
            .Select(line => line.Split(" "))
            .Select(split => (split[0], int.Parse(split[1])))
            .ToList();

        foreach (var (direction, amount) in commands)
        {
            switch (direction)
            {
                case "up":
                    depth -= amount;
                    break;
                case "down":
                    depth += amount;
                    break;
                case "forward":
                    position += amount;
                    break;
                default:
                    DebugMsg($"Unknown direction {direction}");
                    break;
            }
        }

        return (depth * position).ToString();
    }

    public string? SecondPuzzle()
    {
        var depth = 0;
        var position = 0;
        var aim = 0;

        var commands = _input
            .Select(line => line.Split(" "))
            .Select(split => (split[0], int.Parse(split[1])))
            .ToList();

        foreach (var (direction, amount) in commands)
        {
            switch (direction)
            {
                case "up":
                    aim -= amount;
                    break;
                case "down":
                    aim += amount;
                    break;
                case "forward":
                    position += amount;
                    depth += aim * amount;
                    break;
                default:
                    DebugMsg($"Unknown direction {direction}");
                    break;
            }
        }

        return (depth * position).ToString();
    }
}