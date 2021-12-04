package adventofcode.impl

import adventofcode.Puzzle
import adventofcode.Helper

class Day01(filename: String) : Puzzle(filename) {

    override fun solveFirst(): String? {
        val numbers = input
            .map { it.trim() }
            .map { it.toInt() }

        var countIncreases = 0
        for (i in 1 until numbers.size) {
            if (numbers[i] > numbers[i - 1]) countIncreases++
        }

        Helper.debugMsg("There was ${countIncreases} out of total ${numbers.size} numbers.");
        return "$countIncreases"
    }

    override fun solveSecond(): String? {

        val numbers = input
            .map { it.trim() }
            .map { it.toInt() }

        val zipped = numbers.windowed(3)
        val summed = zipped.map { it.sum() }

        var countIncreases = 0
        for (i in 1 until summed.size) {
            if (summed[i] > summed[i - 1]) countIncreases++
        }

        Helper.debugMsg("There was ${countIncreases} out of total ${summed.size} tuples.");
        return "$countIncreases"
    }
}
