using System.Collections.Immutable;
using static csharp.Helper;

namespace csharp.impl;

public class Day13 : IPuzzle
{
    private readonly string[] _input;
    private const string FoldString = "fold along ";
    private readonly Func<bool, string> _defaultMapper = isP => isP ? "#" : ".";

    public Day13(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var points = _input
            .Select(line => line.Split(","))
            .Where(parse => parse.Length == 2)
            .Select(line => line
                .Select(int.Parse).ToArray())
            .Select(numPair => new Point(numPair[0], numPair[1]))
            .ToList();

        var folds = _input
            .Where(line => line.StartsWith(FoldString))
            .Select(line => line.Replace(FoldString, ""))
            .Select(line => line.Split("="))
            .Select(fold => Enum.TryParse(fold[0], true, out Axis axis) ? new Fold(axis, int.Parse(fold[1])) : null)
            .Where(fold => fold != null)
            .Select(fold => fold!)
            .ToList();


        Dictionary<Point, bool> dict = new();
        points.ForEach(p => dict[p] = true);
        var grid = PointDictToGrid(dict, _defaultMapper);
        DebugMsg($"Map is:\n{GridAsPrintable(grid)}");

        DebugMsg($"First fold instruction is {folds[0]}");
        DoFold(points, folds[0]);
        dict = new();
        points.ForEach(p => dict[p] = true);
        grid = PointDictToGrid(dict, _defaultMapper);
        DebugMsg($"Map is:\n{GridAsPrintable(grid)}");


        var pointsAfterFold = points.Count;
        DebugMsg($"There are still {pointsAfterFold} dots left");

        return pointsAfterFold.ToString();
    }

    public string? SecondPuzzle()
    {
        var points = _input
            .Select(line => line.Split(","))
            .Where(parse => parse.Length == 2)
            .Select(line => line
                .Select(int.Parse).ToArray())
            .Select(numPair => new Point(numPair[0], numPair[1]))
            .ToList();

        var folds = _input
            .Where(line => line.StartsWith(FoldString))
            .Select(line => line.Replace(FoldString, ""))
            .Select(line => line.Split("="))
            .Select(fold => Enum.TryParse(fold[0], true, out Axis axis) ? new Fold(axis, int.Parse(fold[1])) : null)
            .Where(fold => fold != null)
            .Select(fold => fold!)
            .ToList();


        Dictionary<Point, bool> dict = new();
        points.ForEach(p => dict[p] = true);
        var grid = PointDictToGrid(dict, _defaultMapper);
        DebugMsg($"Starting Map is:\n{GridAsPrintable(grid)}");


        foreach (var fold in folds)
        {
            DebugMsg($"Fold instruction is {fold}");
            DoFold(points, fold);
        }

        dict = new();
        points.ForEach(p => dict[p] = true);
        grid = PointDictToGrid(dict, _defaultMapper);
        DebugMsg($"Final map is:\n{GridAsPrintable(grid)}");

        return "Complex output";
    }

    private enum Axis { X, Y }

    private record Fold(Axis Axis, int Place);


    private void DoFold(List<Point> map, Fold fold)
    {
        bool IsInFoldSpace(Point p) =>
            fold.Axis switch
            {
                Axis.X => p.X > fold.Place,
                Axis.Y => p.Y > fold.Place
            };

        var toMove = map.Where(IsInFoldSpace)
            .ToList();
        map.RemoveAll(IsInFoldSpace);
        toMove.ForEach(p =>
        {
            var newPoint = fold.Axis switch
            {
                Axis.X => new Point(2 * fold.Place - p.X, p.Y),
                Axis.Y => new Point(p.X, 2 * fold.Place - p.Y)
            };
            if (map.Contains(newPoint)) return;
            map.Add(newPoint);
        });
    }
}