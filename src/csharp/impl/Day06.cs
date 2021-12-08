using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Text;
using static csharp.Helper;

namespace csharp.impl;

public class Day06 : IPuzzle
{
    private readonly string[] _input;

    private const int DaysToWatchFirstPuzzle = 80;
    private const int DaysToWatchSecondPuzzle = 256;
    private const short Cycle = 7;
    private const short NewFishOffset = 2;
    private const short ExtendedCycle = Cycle + NewFishOffset;

    public Day06(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var input = _input.SelectMany(line =>
                line.Split(",")
                    .Select(short.Parse))
            .ToList();

        DebugMsg($"Initial state: {string.Join(",", input)}");

        var fishCountPerCycle = new long[NewFishOffset];
        input.ForEach(fish => fishCountPerCycle[fish] += 1);

        for (var day = 0; day < DaysToWatchFirstPuzzle; day++)
        {
            var fishCountParents = fishCountPerCycle[0];
            for (var idx = 1; idx < ExtendedCycle; idx++)
            {
                fishCountPerCycle[idx - 1] = fishCountPerCycle[idx];
            }

            fishCountPerCycle[Cycle - 1] += fishCountParents;
            fishCountPerCycle[^1] = fishCountParents;
        }

        var result = fishCountPerCycle.Sum();
        DebugMsg($"There are {result} fish alive after {DaysToWatchFirstPuzzle} days.");
        return result.ToString();
    }

    public string? SecondPuzzle()
    {
        var input = _input.SelectMany(line =>
                line.Split(",")
                    .Select(short.Parse))
            .ToList();

        DebugMsg($"Initial state: {string.Join(",", input)}");

        var fishCountPerCycle = new long[NewFishOffset];
        input.ForEach(fish => fishCountPerCycle[fish] += 1);

        for (var day = 0; day < DaysToWatchSecondPuzzle; day++)
        {
            var fishCountParents = fishCountPerCycle[0];
            for (var idx = 1; idx < ExtendedCycle; idx++)
            {
                fishCountPerCycle[idx - 1] = fishCountPerCycle[idx];
            }

            fishCountPerCycle[Cycle - 1] += fishCountParents;
            fishCountPerCycle[^1] = fishCountParents;
        }

        var result = fishCountPerCycle.Sum();
        DebugMsg($"There are {result} fish alive after {DaysToWatchSecondPuzzle} days.");
        return result.ToString();
    }
}