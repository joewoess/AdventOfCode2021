using static csharpimpl.Helper;

namespace csharpimpl.puzzleImpl;

public class Day01 : IPuzzle
{
    private readonly string[] _input;
    public Day01(string filename)
    {
        _input = File.ReadAllLines(filename);
    }
    
    public string? FirstPuzzle()
    {
        var numbers = _input
            .Select(line => line.Trim())
            .Select(int.Parse).ToList();

        var countIncreases = 0;
        for (int i = 1; i < numbers.Count; i++) {
            if(numbers[i] > numbers[i - 1]) {
                countIncreases++;
            } 
        }

        DebugMsg($"There was {countIncreases} out of total {numbers.Count} numbers.");
        return countIncreases.ToString();
    }

    public string? SecondPuzzle()
    {        
        var numbers = _input
            .Select(line => line.Trim())
            .Select(int.Parse).ToList();

        var zipped = numbers.Zip(numbers.Skip(1), (a, b) => new { a, b })
            .Zip(numbers.Skip(2), (ab, c) =>  (ab.a, ab.b, c));

        var summed = zipped.Select(tuple => tuple.a + tuple.b + tuple.c).ToList();

        var countIncreases = 0;
        for (int i = 1; i < summed.Count; i++) {
            if(summed[i] > summed[i - 1]) {
                countIncreases++;
            } 
        }

        DebugMsg($"There was {countIncreases} out of total {summed.Count} tuples.");
        return countIncreases.ToString();
    }
}