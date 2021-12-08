using System;
using System.Text;
using static csharp.Helper;

namespace csharp.impl;

public class Day07 : IPuzzle
{
    private readonly string[] _input;

    public Day07(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var input = _input.SelectMany(line =>
                line.Split(",")
                    .Select(short.Parse))
            .ToList();

        var min = input.Min();
        var max = input.Max();

        var minOffset = int.MaxValue;
        for (int i = min; i <= max; i++)
        {
            minOffset = Math.Min(minOffset, input.Sum(pos => Math.Abs(i - pos)));
        }

        return minOffset.ToString();
    }

    public string? SecondPuzzle()
    {
        var input = _input.SelectMany(line =>
                line.Split(",")
                    .Select(short.Parse))
            .ToList();

        var min = input.Min();
        var max = input.Max();

        var minOffset = int.MaxValue;
        for (int i = min; i <= max; i++)
        {
            minOffset = Math.Min(minOffset, input.Sum(pos => Enumerable.Range(0, Math.Abs(i - pos) + 1).Sum()));
        }

        return minOffset.ToString();
    }
}