using static csharp.Helper;

namespace csharp.impl;

public class Day09 : IPuzzle
{
    private readonly string[] _input;

    public Day09(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var input = _input.Select(line =>
                line.ToList()
                    .Select(char.GetNumericValue)
                    .Select(Convert.ToInt32)
                    .ToArray())
            .ToArray();

        var lowPoints = new List<int>();

        for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
        {
            for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
            {
                if (input[lineIdx][posIdx] < GetNeighbors(input, lineIdx, posIdx).Min())
                {
                    lowPoints.Add(input[lineIdx][posIdx]);
                    DebugMsg($"Found low point at ({lineIdx},{posIdx}) with height {input[lineIdx][posIdx]}");
                }
            }
        }

        var result = lowPoints.Count + lowPoints.Sum();
        DebugMsg($"Sum of risk levels is {result} ");
        return result.ToString();
    }

    public string? SecondPuzzle()
    {
        var input = _input.Select(line =>
                line.ToList()
                    .Select(char.GetNumericValue)
                    .Select(Convert.ToInt32)
                    .ToArray())
            .ToArray();

        var basinSizes = new List<int>();
        for (var lineIdx = 0; lineIdx < input.Length; lineIdx++)
        {
            for (var posIdx = 0; posIdx < input[lineIdx].Length; posIdx++)
            {
                if (input[lineIdx][posIdx] < GetNeighbors(input, lineIdx, posIdx).Min())
                {
                    basinSizes.Add(GetBasin(input, lineIdx, posIdx).Count);
                    DebugMsg($"Found basin of size {basinSizes.Last()} at ({lineIdx},{posIdx})");
                }
            }
        }

        var biggestBasins = basinSizes.OrderByDescending(it => it).Take(3).ToList();
        var result = biggestBasins.Aggregate(1, (acc, val) => acc * val);
        DebugMsg($"Largest 3 basins {biggestBasins[0]} {biggestBasins[1]} {biggestBasins[2]} result in {result}");
        return result.ToString();
    }

    private static IEnumerable<int> GetNeighbors(int[][] input, int lineIdx, int posIdx)
    {
        var neighbors = new List<int>();
        if (lineIdx > 0)
        {
            neighbors.Add(input[lineIdx - 1][posIdx]);
        }

        if (lineIdx < input.Length - 1)
        {
            neighbors.Add(input[lineIdx + 1][posIdx]);
        }

        if (posIdx > 0)
        {
            neighbors.Add(input[lineIdx][posIdx - 1]);
        }

        if (posIdx < input[lineIdx].Length - 1)
        {
            neighbors.Add(input[lineIdx][posIdx + 1]);
        }

        return neighbors;
    }

    private List<(int X, int Y)> GetBasin(int[][] input, int lineIdx, int posIdx)
    {
        var basin = new List<(int X, int Y)> { (lineIdx, posIdx) };
        if (lineIdx > 0
            && input[lineIdx - 1][posIdx] > input[lineIdx][posIdx]
            && input[lineIdx - 1][posIdx] != 9)
        {
            basin.AddRange(GetBasin(input, lineIdx - 1, posIdx));
        }

        if (lineIdx < input.Length - 1
            && input[lineIdx + 1][posIdx] > input[lineIdx][posIdx]
            && input[lineIdx + 1][posIdx] != 9)
        {
            basin.AddRange(GetBasin(input, lineIdx + 1, posIdx));
        }

        if (posIdx > 0
            && input[lineIdx][posIdx - 1] > input[lineIdx][posIdx]
            && input[lineIdx][posIdx - 1] != 9)
        {
            basin.AddRange(GetBasin(input, lineIdx, posIdx - 1));
        }

        if (posIdx < input[lineIdx].Length - 1
            && input[lineIdx][posIdx + 1] > input[lineIdx][posIdx]
            && input[lineIdx][posIdx + 1] != 9)
        {
            basin.AddRange(GetBasin(input, lineIdx, posIdx + 1));
        }

        return basin.Distinct().ToList();
    }
}