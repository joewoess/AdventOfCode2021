using System.Collections.Immutable;
using static csharp.Helper;

namespace csharp.impl;

public class Day12 : IPuzzle
{
    private readonly string[] _input;
    private const string Start = "start";
    private const string End = "end";

    public Day12(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var dict = new Dictionary<string, List<string>>();

        foreach (var (from, to) in _input.Select(line => line.Split("-")).Select(names => (names[0], names[1])))
        {
            dict[from] = dict.GetValueOrDefault(from, new List<string>()).Append(to).ToList();
            dict[to] = dict.GetValueOrDefault(to, new List<string>()).Append(from).ToList();
        }

        DebugMsg($"Connections are:\n{string.Join("\n", dict.Select(kvp => $"{kvp.Key} -> {string.Join(", ", kvp.Value)}"))}");

        var paths = new List<string>();
        var visited = new Dictionary<string, int>();
        dict.Keys.ToList().ForEach(key => visited[key] = 0);

        paths.AddRange(FindPath(dict, Start, visited));

        DebugMsg($"Total paths: {paths.Count}");
        paths.ForEach(path => DebugMsg("Path: " + path));

        return paths.Count.ToString();
    }

    public string? SecondPuzzle()
    {
        var dict = new Dictionary<string, List<string>>();

        foreach (var (from, to) in _input.Select(line => line.Split("-")).Select(names => (names[0], names[1])))
        {
            dict[from] = dict.GetValueOrDefault(from, new List<string>()).Append(to).ToList();
            dict[to] = dict.GetValueOrDefault(to, new List<string>()).Append(from).ToList();
        }

        DebugMsg($"Connections are:\n{string.Join("\n", dict.Select(kvp => $"{kvp.Key} -> {string.Join(", ", kvp.Value)}"))}");

        var paths = new List<string>();
        var visited = new Dictionary<string, int>();
        dict.Keys.ToList().ForEach(key => visited[key] = 0);

        paths.AddRange(FindPath(dict, Start, visited, false));

        DebugMsg($"Total paths: {paths.Count}");
        paths.ForEach(path => DebugMsg("Path: " + path));

        return paths.Count.ToString();
    }

    private List<string> FindPath(IReadOnlyDictionary<string, List<string>> dict, string currentNode, IDictionary<string, int> visitedPaths, bool usedSmallCaveInspection = true)
    {
        visitedPaths[currentNode]++;
        switch (currentNode)
        {
            case Start when visitedPaths[currentNode] > 1:
                return new List<string>();
            case End:
                visitedPaths[currentNode]--;
                return new List<string> { currentNode };
            case var c when char.IsLower(c[0]) && visitedPaths[currentNode] > 1 && usedSmallCaveInspection:
                visitedPaths[currentNode]--;
                return new List<string>();
            case var c when char.IsLower(c[0]) && visitedPaths[currentNode] > 1 && !usedSmallCaveInspection:
                usedSmallCaveInspection = true;
                break;
        }

        var paths = new List<string>();

        foreach (var node in dict[currentNode])
        {
            var result = FindPath(dict, node, visitedPaths, usedSmallCaveInspection);
            if (result.Any())
            {
                paths.AddRange(result.Select(p => currentNode + "," + p));
            }
        }

        visitedPaths[currentNode]--;
        return paths;
    }
}