package adventofcode.impl

import adventofcode.Puzzle
import adventofcode.Helper

class Day03(filename: String) : Puzzle(filename) {

    override fun solveFirst(): String? {
        val diagnostics = input.map { DiagnosticStat(it, it.length, it.bitsToInt()) }

        val commonLength = diagnostics[0].numBits
        if (commonLength == 0 || !diagnostics.all { it.numBits == commonLength }) {
            Helper.debugMsg("Input did not have common length $commonLength")
            return null
        }

        var gammaRate = "";
        var epsilonRate = "";

        for (bitIdx in 0 until commonLength) {
            val (ones, zeros) = countBits(diagnostics, bitIdx)
            gammaRate += (ones > zeros).asChar()
            epsilonRate += (zeros > ones).asChar()
        }

        Helper.debugMsg("Gamma rate was '$gammaRate' or ${gammaRate.bitsToInt()}")
        Helper.debugMsg("Epsilon rate was '$epsilonRate' or ${epsilonRate.bitsToInt()}")

        return "${gammaRate.bitsToInt() * epsilonRate.bitsToInt()}"
    }

    override fun solveSecond(): String? {
        val diagnostics = input.map { DiagnosticStat(it, it.length, it.bitsToInt()) }

        val commonLength = diagnostics[0].numBits
        if (commonLength == 0 || !diagnostics.all { it.numBits == commonLength }) {
            Helper.debugMsg("Input did not have common length $commonLength")
            return null
        }

        val remainingOxygenGenerator = diagnostics.toMutableList()
        val remainingLifeSupport = diagnostics.toMutableList()

        for(bitIdx in 0 until commonLength) {
            if (remainingOxygenGenerator.size > 1) {
                val (ones, zeros) = countBits(remainingOxygenGenerator, bitIdx)
                Helper.debugMsg("Therer are $ones 1s and $zeros 0s remaining in oxygen")
                remainingOxygenGenerator -= { it.statBits[bitIdx] != (ones >= zeros).asChar() }
            }
            if (remainingLifeSupport.size > 1) {
                val (ones, zeros) = countBits(remainingLifeSupport, bitIdx)
                Helper.debugMsg("Therer are $ones 1s and $zeros 0s remaining in lifesupp")
                remainingLifeSupport -= { it.statBits[bitIdx] != (ones < zeros).asChar() }
            }
        }

        if (remainingOxygenGenerator.size == 0 || remainingLifeSupport.size == 0) {
            Helper.debugMsg("There was no valid entries for the bit criteria")
            return null
        }

        val oxygenGeneratorRating = remainingOxygenGenerator[0]
        val lifeSupportRating = remainingLifeSupport[0]

        Helper.debugMsg("Oxygen generator rating was '${oxygenGeneratorRating.statBits}' or ${oxygenGeneratorRating.asNumber}")
        Helper.debugMsg("Life support rating was '${lifeSupportRating.statBits}' or ${lifeSupportRating.asNumber}")

        return "${oxygenGeneratorRating.asNumber * lifeSupportRating.asNumber}"
    }

    fun String.bitsToInt(): Int {
        return this.toInt(2)
    }

    fun countBits(remaining: List<DiagnosticStat>, bitIdx: Int): Pair<Int, Int> {
        val remainingOnes = remaining.filter { it.statBits[bitIdx] == '1' }.size
        return Pair(remainingOnes, remaining.size - remainingOnes)
    }

    fun Boolean.asChar(): Char { 
        return if (this) '1' else '0' 
    }

    operator fun MutableList<DiagnosticStat>.minusAssign(filter: (DiagnosticStat) -> Boolean) { 
        this.removeAll { filter(it) }
    }
}

data class DiagnosticStat(val statBits: String, val numBits: Int, val asNumber: Int)
