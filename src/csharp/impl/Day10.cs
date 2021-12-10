using static csharp.Helper;

namespace csharp.impl;

public class Day10 : IPuzzle
{
    private readonly string[] _input;
    private static readonly Dictionary<char, char> Tokens = new() { { '{', '}' }, { '(', ')' }, { '[', ']' }, { '<', '>' } };
    private static readonly Dictionary<char, int> MissingScore = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
    private static readonly Dictionary<char, int> ClosingScore = new() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
    private static readonly Func<string, long> ScoreFunc = str => str.Aggregate(0L, (acc, val) => acc * 5L + ClosingScore[val]);

    public Day10(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var corruptedInput = _input.Select(line =>
                FirstInvalid(line, 0, new List<char>()))
            .Where(invalid => invalid.corrupted != null)
            .ToArray();
        var scores = corruptedInput
            .Select(invalid => MissingScore[invalid.corrupted!.Value])
            .ToArray();

        foreach (var (corrupted, _) in corruptedInput)
        {
            DebugMsg($"Incorrect symbol is {corrupted!.Value}");
        }

        return scores.Sum().ToString();
    }

    public string? SecondPuzzle()
    {
        var incompleteInput = _input.Select(line =>
                FirstInvalid(line, 0, new List<char>()))
            .Where(incomplete => incomplete.corrupted == null)
            .Where(incomplete => incomplete.incomplete != null)
            .ToArray();
        var scores = incompleteInput
            .Select(incomplete => ScoreFunc(incomplete.incomplete!))
            .ToArray();

        foreach (var (_, incomplete) in incompleteInput)
        {
            DebugMsg($"Missing closing symbol {incomplete} => {ScoreFunc(incomplete!)}");
        }

        var middleScore = scores.OrderBy(it => it).ElementAt((scores.Length - 1) / 2);
        DebugMsg($"Score was {middleScore}");

        return middleScore.ToString();
    }

    private static (char? corrupted, string? incomplete) FirstInvalid(string line, int idx, List<char> currentOpenScope)
    {
        if (idx >= line.Length)
            return currentOpenScope.Count > 0
                ? (null, new string(currentOpenScope.Select(opening => Tokens[opening]).Reverse().ToArray()))
                : (null, null);
        return line[idx] switch
        {
            var c when Tokens.ContainsKey(c) => FirstInvalid(line, idx + 1, currentOpenScope.Append(c).ToList()),
            var c when currentOpenScope.Count != 0 && c == Tokens[currentOpenScope[^1]] => FirstInvalid(line, idx + 1, currentOpenScope.SkipLast(1).ToList()),
            _ => (line[idx], new string(currentOpenScope.ToArray()))
        };
    }
}