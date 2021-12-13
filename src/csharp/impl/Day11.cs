using static csharp.Helper;

namespace csharp.impl;

public class Day11 : IPuzzle
{
    private readonly string[] _input;
    private const int Iterations = 100;
    private readonly Func<(int, bool), string> _defaultMapper = val => val.Item2 ? "X" : val.Item1.ToString();

    public Day11(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var input = _input.Select(line =>
                line.ToList()
                    .Select(char.GetNumericValue)
                    .Select(Convert.ToInt32)
                    .Select(num => (num: num, hasFlashed: false))
                    .ToArray())
            .ToArray();


        var flashes = 0;
        DebugMsg($"Input energy levels are:\n{GridAsPrintable(input, _defaultMapper)}");

        foreach (var iter in Enumerable.Range(1, Iterations))
        {
            for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
            {
                for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
                {
                    CheckFlash(input, lineIdx, posIdx);
                }
            }

            DebugMsg($"Energy level after {iter} iterations are:\n{GridAsPrintable(input, _defaultMapper)}");
            flashes += input.Sum(line => line.Sum(pos => pos.hasFlashed ? 1 : 0));

            for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
            {
                for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
                {
                    if (input[lineIdx][posIdx].hasFlashed) input[lineIdx][posIdx] = (0, false);
                }
            }
        }

        DebugMsg($"There was {flashes} flashes in {Iterations} iterations");
        return flashes.ToString();
    }

    public string? SecondPuzzle()
    {
        var input = _input.Select(line =>
                line.ToList()
                    .Select(char.GetNumericValue)
                    .Select(Convert.ToInt32)
                    .Select(num => (num: num, hasFlashed: false))
                    .ToArray())
            .ToArray();

        DebugMsg($"Input energy levels are:\n{GridAsPrintable(input, _defaultMapper)}");

        foreach (var iter in Enumerable.Range(1, Iterations * Iterations)) // Iterations ^ 2 is the timeout limit here, perfectly correct would be a while()
        {
            for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
            {
                for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
                {
                    CheckFlash(input, lineIdx, posIdx);
                }
            }

            if (input.All(line => line.All(val => val.hasFlashed)))
            {
                DebugMsg($"First total flash at {iter} iterations");
                return iter.ToString();
            }

            for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
            {
                for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
                {
                    if (input[lineIdx][posIdx].hasFlashed) input[lineIdx][posIdx] = (0, false);
                }
            }
        }

        DebugMsg($"There was no total flash");
        return null;
    }

    private void CheckFlash((int num, bool hasFlashed)[][] input, int lineIdx, int posIdx)
    {
        if (lineIdx < 0 || lineIdx >= input.Length || posIdx < 0 || posIdx >= input[lineIdx].Length) return;
        input[lineIdx][posIdx] = (input[lineIdx][posIdx].num + 1, input[lineIdx][posIdx].hasFlashed);
        if (input[lineIdx][posIdx].num > 9 && !input[lineIdx][posIdx].hasFlashed)
        {
            input[lineIdx][posIdx].hasFlashed = true;
            CheckFlash(input, lineIdx - 1, posIdx);
            CheckFlash(input, lineIdx + 1, posIdx);
            CheckFlash(input, lineIdx, posIdx - 1);
            CheckFlash(input, lineIdx, posIdx + 1);
            CheckFlash(input, lineIdx - 1, posIdx + 1);
            CheckFlash(input, lineIdx - 1, posIdx - 1);
            CheckFlash(input, lineIdx + 1, posIdx + 1);
            CheckFlash(input, lineIdx + 1, posIdx - 1);
        }
    }
}