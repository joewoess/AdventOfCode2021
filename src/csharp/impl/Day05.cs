using System;
using System.Text;
using static csharp.Helper;

namespace csharp.impl;

public class Day05 : IPuzzle
{
    private readonly string[] _input;
    private readonly Func<int, string> _defaultMapper = val => val == 0 ? "." : val.ToString();

    public Day05(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var vents = _input.Select(line => line.Split(" -> ")
                .Select(numPair => numPair.Split(",").Select(int.Parse).ToArray())
                .Select(nums => new Point(nums[0], nums[1]))
                .ToArray())
            .Select(points => new Vent(points[0], points[1]))
            .ToList();
        DebugMsg($"Vents are:\n{string.Join("\n", vents)}");

        var onlySimpleVents = vents.Where(vent => vent.From.X == vent.To.X || vent.From.Y == vent.To.Y).ToList();
        DebugMsg($"Simple Vents are:\n{string.Join("\n", onlySimpleVents)}");

        var map = new Dictionary<Point, int>();
        foreach (var (from, to) in onlySimpleVents)
        {
            foreach (var key in Walk(from, to))
            {
                map[key] = map.GetValueOrDefault(key) + 1;
            }
        }

        var grid = PointDictToGrid(map, _defaultMapper);
        DebugMsg($"Map is:\n{GridAsPrintable(grid)}");

        return map.Values.Count(crosses => crosses >= 2).ToString();
    }

    public string? SecondPuzzle()
    {
        var vents = _input.Select(line => line.Split(" -> ")
                .Select(numPair => numPair.Split(",").Select(int.Parse).ToArray())
                .Select(nums => new Point(nums[0], nums[1]))
                .ToArray())
            .Select(points => new Vent(points[0], points[1]))
            .ToList();
        DebugMsg($"Vents are:\n{string.Join("\n", vents)}");

        var map = new Dictionary<Point, int>();
        foreach (var (from, to) in vents)
        {
            foreach (var key in Walk(from, to))
            {
                map[key] = map.GetValueOrDefault(key) + 1;
            }
        }

        var grid = PointDictToGrid(map, _defaultMapper);
        DebugMsg($"Map is:\n{GridAsPrintable(grid)}");

        return map.Values.Count(crosses => crosses >= 2).ToString();
    }


    private readonly record struct Vent(Point From, Point To)
    {
        public override string ToString() => $"{From} -> {To}";
    };
}