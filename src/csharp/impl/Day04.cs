using static csharp.Helper;

namespace csharp.impl;

public class Day04 : IPuzzle
{
    private readonly string[] _input;

    public Day04(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var drawNumbers = _input.FirstOrDefault()?.Split(",").Select(int.Parse).ToList() ?? new List<int>();
        DebugMsg($"Draw numbers are: {string.Join(",", drawNumbers)}");

        var boards = _input
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Chunk(5)
            .Select(board => new Board(board))
            .ToList();

        DebugMsg($"Board #1: \n{boards[0]}");

        foreach (var num in drawNumbers)
        {
            foreach (var board in boards)
            {
                board.MarkIfThere(num);
                if (board.IsFinished)
                {
                    var finishingLine = board.FinishingLine ?? Array.Empty<MarkableNumber>();
                    DebugMsg(
                        $"Finished at #{num} with sum {board.SumOfUnmarked()} line {string.Join(",", finishingLine)}");
                    return (board.SumOfUnmarked() * num).ToString();
                }
            }
        }

        return null;
    }

    public string? SecondPuzzle()
    {
        var drawNumbers = _input.FirstOrDefault()?.Split(",").Select(int.Parse).ToList() ?? new List<int>();
        DebugMsg($"Draw numbers are: {string.Join(",", drawNumbers)}");

        var boards = _input
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Chunk(5)
            .Select(board => new Board(board))
            .ToList();

        DebugMsg($"Board #1: \n{boards[0]}");

        foreach (var num in drawNumbers)
        {
            foreach (var board in boards)
            {
                board.MarkIfThere(num);
                if (boards.Count(b => !b.IsFinished) == 0)
                {
                    var finishingLine = board.FinishingLine ?? Array.Empty<MarkableNumber>();
                    DebugMsg(
                        $"Finished at #{num} with sum {board.SumOfUnmarked()} line {string.Join(",", finishingLine)}");
                    return (board.SumOfUnmarked() * num).ToString();
                }
            }
        }

        return null;
    }

    public record struct MarkableNumber(int Number, bool Marked = false)
    {
        public override string ToString()
        {
            return $"{Number,2:##}";
        }
    }

    private class Board
    {
        public Board(string[] inputData)
        {
            BoardNumbers = new MarkableNumber[inputData.Length][];
            for (int lineIdx = 0; lineIdx < inputData.Length; lineIdx++)
            {
                var lineNumbers = inputData[lineIdx].Split(" ").Where(num => !string.IsNullOrWhiteSpace(num)).ToArray();
                BoardNumbers[lineIdx] = new MarkableNumber[lineNumbers.Length];
                for (int numIdx = 0; numIdx < lineNumbers.Length; numIdx++)
                {
                    BoardNumbers[lineIdx][numIdx] = new MarkableNumber(int.Parse(lineNumbers[numIdx]));
                }
            }
        }

        private MarkableNumber[][] BoardNumbers { get; set; }

        public void MarkIfThere(int number)
        {
            foreach (var line in BoardNumbers)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i].Number != number) continue;
                    line[i].Marked = true;
                    if (line.All(markable => markable.Marked))
                    {
                        IsFinished = true;
                        FinishingLine = line;
                        return;
                    }

                    var vertical = BoardNumbers.Select(l => l[i]).ToArray();
                    if (!vertical.All(markable => markable.Marked)) return;
                    
                    IsFinished = true;
                    FinishingLine = vertical.ToArray();
                    return;
                }
            }
        }

        public bool IsFinished { get; private set; } = false;
        public MarkableNumber[]? FinishingLine { get; private set; } = null;

        public int SumOfUnmarked()
        {
            return BoardNumbers.Select(line => line.Where(num => !num.Marked).Sum(num => num.Number)).Sum();
        }

        public override string ToString()
        {
            return string.Join("\n", BoardNumbers.Select(line => string.Join(" ", line)));
        }
    }
}