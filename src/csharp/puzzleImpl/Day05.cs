using System.Text;
using static csharp.Helper;

namespace csharp.puzzleImpl;

public class Day05 : IPuzzle
{
    private readonly string[] _input;

    public Day05(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle() => null;
    public string? SecondPuzzle() => null;
}