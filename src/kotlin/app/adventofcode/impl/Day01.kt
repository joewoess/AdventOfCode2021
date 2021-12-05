package adventofcode.impl

import adventofcode.Puzzle
import adventofcode.Helper

class Day01(filename: String) : Puzzle(filename) {

    override fun solveFirst(): String? {
        val numbers = input
            .map { it.trim() }
            .map { it.toInt() }

        val increases = countIncreases(numbers)
        Helper.debugMsg("There was $increases out of total ${numbers.size} numbers.");
        return "$increases"
    }

    override fun solveSecond(): String? {

        val numbers = input
            .map { it.trim() }
            .map { it.toInt() }

        val zipped = numbers.windowed(3)
        val summed = zipped.map { it.sum() }

        val increases = countIncreases(summed)
        Helper.debugMsg("There was $increases out of total ${summed.size} tuples.");
        return "$increases"
    }

    private fun <T : Comparable<T>> countIncreases(collection: List<T>): Int = (1 until collection.size).sumOf { (collection[it] > collection[it - 1]).toInt() }

    private fun Boolean.toInt() = if (this) 1 else 0
}
