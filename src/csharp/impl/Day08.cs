using static csharp.Helper;

namespace csharp.impl;

public class Day08 : IPuzzle
{
    private readonly string[] _input;

    public Day08(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var input = _input.Select(line =>
                line.Split("|")
                    .Select(entryPart => entryPart.Trim().Split(" ").ToArray())
                    .ToArray())
            .Select(entry => new FourDigitNumberEntry(entry[0], entry[1])).ToList();

        var uniqueOverall = 0;
        foreach (var entry in input)
        {
            var uniqueInActual = entry.ReturnUnique(true).Length;
            uniqueOverall += uniqueInActual;
            DebugMsg($"{entry} => {uniqueInActual}");
        }

        return uniqueOverall.ToString();
    }

    public string? SecondPuzzle()
    {
        var input = _input.Select(line =>
                line.Split("|")
                    .Select(entryPart => entryPart.Trim().Split(" ").ToArray())
                    .ToArray())
            .Select(entry => new FourDigitNumberEntry(entry[0], entry[1])).ToList();

        var sumOfNumbers = 0;
        foreach (var entry in input)
        {
            var actualNumber = entry.GetActualNumber();
            sumOfNumbers += actualNumber;
            DebugMsg($"{entry} => {actualNumber}");
        }

        return sumOfNumbers.ToString();
    }

    private record FourDigitNumberEntry(string[] TrialDigits, string[] ActualDigits)
    {
        private static readonly int[] UniqueNumberLengths = { 2, 3, 4, 7 };
        private static readonly List<string> SegmentToNumberMapping = new() { "abcefg", "cf", "acdeg", "acdfg", "bcdf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg" };

        public string[] ReturnUnique(bool onlyInActual = false)
        {
            return onlyInActual
                ? ActualDigits.Where(num => UniqueNumberLengths.Contains(num.Length)).ToArray()
                : AllDigits.Where(num => UniqueNumberLengths.Contains(num.Length)).ToArray();
        }

        private IEnumerable<string> AllDigits => TrialDigits.Concat(ActualDigits).ToArray();

        private Dictionary<char, char> FigureOutMapping()
        {
            var uniqueStrings = ReturnUnique();
            var segmentMapping = new Dictionary<char, char>();

            var oneMap = uniqueStrings.First(num => num.Length == 2);
            var sevenMap = uniqueStrings.First(num => num.Length == 3);
            var fourMap = uniqueStrings.First(num => num.Length == 4);
            var eightMap = uniqueStrings.First(num => num.Length == 7);
            var fiveLengths = AllDigits.Where(num => num.Length == 5).ToList();
            var sixLengths = AllDigits.Where(num => num.Length == 6).ToList();
            var inAllFiveLengths = new string(eightMap.Where(ch => fiveLengths.All(num => num.Contains(ch))).ToArray());
            var inAllSixLengths = new string(eightMap.Where(ch => sixLengths.All(num => num.Contains(ch))).ToArray());

            var mappingForA = sevenMap.Except(oneMap).First();
            var mappingForE = eightMap.Except(inAllFiveLengths).Except(inAllSixLengths).Except(oneMap).First();
            var mappingForG = eightMap.Except(fourMap).Except(mappingForA.ToString()).Except(mappingForE.ToString()).First();
            var mappingForD = eightMap.Where(ch => fiveLengths.All(num => num.Contains(ch))).Except(mappingForA.ToString()).Except(mappingForG.ToString()).First();
            var mappingForB = fourMap.Except(oneMap).Except(mappingForD.ToString()).First();
            var mappingForF = eightMap.Where(ch => sixLengths.All(num => num.Contains(ch)))
                .Except(mappingForA.ToString())
                .Except(mappingForB.ToString())
                .Except(mappingForG.ToString())
                .First();
            var mappingForC = oneMap.Except(mappingForF.ToString()).First();

            segmentMapping[mappingForA] = 'a';
            segmentMapping[mappingForB] = 'b';
            segmentMapping[mappingForC] = 'c';
            segmentMapping[mappingForD] = 'd';
            segmentMapping[mappingForE] = 'e';
            segmentMapping[mappingForF] = 'f';
            segmentMapping[mappingForG] = 'g';
            return segmentMapping;
        }

        public int GetActualNumber()
        {
            var mapping = FigureOutMapping();
            var remapped = ActualDigits
                .Select(num => new string(num
                    .Select(ch => mapping[ch])
                    .OrderBy(ch => ch)
                    .ToArray())).ToArray();
            var digits = remapped.Select(str => SegmentToNumberMapping.IndexOf(str)).ToArray();

            return int.Parse(string.Join("", digits));
        }

        public override string ToString()
        {
            return $"{string.Join(" ", TrialDigits)} | {string.Join(" ", ActualDigits)}";
        }
    }
}