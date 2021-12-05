using System.Text;
using static csharp.Helper;

namespace csharp.impl;

public class Day03 : IPuzzle
{
    private readonly string[] _input;

    public Day03(string filename)
    {
        _input = File.ReadAllLines(filename);
    }

    public string? FirstPuzzle()
    {
        var diagnostics = _input
            .Select(line => new DiagnosticStat(line, line.Length, line.BitsToInt()))
            .ToList();

        var commonLength = diagnostics.FirstOrDefault().NumBits;
        if (commonLength == 0 || !diagnostics.TrueForAll(stat => stat.NumBits == commonLength))
        {
            DebugMsg($"Input did not have common length {commonLength}");
            return null;
        }

        var gammaRateBuilder = new StringBuilder();
        var epsilonRateBuilder = new StringBuilder();

        for (int bitIdx = 0; bitIdx < commonLength; bitIdx++)
        {
            var count = CountBits(diagnostics, bitIdx);
            gammaRateBuilder.Append(BoolToChar(count.ones > count.zeros));
            epsilonRateBuilder.Append(BoolToChar(count.zeros > count.ones));
        }

        var gammaRate = gammaRateBuilder.ToString();
        var epsilonRate = epsilonRateBuilder.ToString();

        DebugMsg($"Gamma rate was '{gammaRate}' or {gammaRate.BitsToInt()}");
        DebugMsg($"Epsilon rate was '{epsilonRate}' or {epsilonRate.BitsToInt()}");

        return (gammaRate.BitsToInt() * epsilonRate.BitsToInt()).ToString();
    }

    public string? SecondPuzzle()
    {
        var diagnostics = _input
            .Select(line => new DiagnosticStat(line, line.Length, line.BitsToInt()))
            .ToList();

        var commonLength = diagnostics.FirstOrDefault().NumBits;
        if (commonLength == 0 || !diagnostics.TrueForAll(stat => stat.NumBits == commonLength))
        {
            DebugMsg($"Input did not have common length {commonLength}");
            return null;
        }

        var remainingOxygenGenerator = diagnostics;
        var remainingLifeSupport = diagnostics;

        for (int bitIdx = 0; bitIdx < commonLength; bitIdx++)
        {
            if (remainingOxygenGenerator.Count > 1)
            {
                var countsOG = CountBits(remainingOxygenGenerator, bitIdx);
                DebugMsg($"Therer are {countsOG.ones} 1s and {countsOG.zeros} 0s remaining in oxygen");

                var criteria = BoolToChar(countsOG.ones >= countsOG.zeros);
                remainingOxygenGenerator =
                    remainingOxygenGenerator.Where(stat => stat.StatBits[bitIdx] == criteria).ToList();
            }

            if (remainingLifeSupport.Count > 1)
            {
                var countsLS = CountBits(remainingLifeSupport, bitIdx);
                DebugMsg($"Therer are {countsLS.ones} 1s and {countsLS.zeros} 0s remaining in lifesupp");

                var criteria = BoolToChar(countsLS.ones < countsLS.zeros);
                remainingLifeSupport = remainingLifeSupport.Where(stat => stat.StatBits[bitIdx] == criteria).ToList();
            }
        }

        if (remainingOxygenGenerator.Count == 0 || remainingLifeSupport.Count == 0)
        {
            DebugMsg($"There was no valid entries for the bit criteria");
            return null;
        }

        var oxygenGeneratorRating = remainingOxygenGenerator.First();
        var lifeSupportRating = remainingLifeSupport.First();

        DebugMsg($"Oxygen generator rating was '{oxygenGeneratorRating.StatBits}' or {oxygenGeneratorRating.AsNumber}");
        DebugMsg($"Life support rating was '{lifeSupportRating.StatBits}' or {lifeSupportRating.AsNumber}");

        return (oxygenGeneratorRating.AsNumber * lifeSupportRating.AsNumber).ToString();
    }

    private static (int ones, int zeros) CountBits(IReadOnlyCollection<DiagnosticStat> remaining, int bitIdx)
    {
        var remainingOnes = remaining.Count(stat => stat.StatBits[bitIdx] == '1');
        return (remainingOnes, remaining.Count - remainingOnes);
    }

    private static char BoolToChar(bool condition) => condition ? '1' : '0';

    private readonly record struct DiagnosticStat(string StatBits, int NumBits, int AsNumber);
}