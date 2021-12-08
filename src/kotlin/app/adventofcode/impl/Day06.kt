package adventofcode.impl

import adventofcode.Helper
import adventofcode.Puzzle

class Day06(filename: String) : Puzzle(filename) {

    private val _daysToWatchFirstPuzzle = 80
    private val _daysToWatchSecondPuzzle = 256
    private val _cycle = 7
    private val _newFishOffset = 2
    private val _newFishValue = _cycle + _newFishOffset - 1
    private val _cycleRange = 1 until _cycle + _newFishOffset

    override fun solveFirst(): String? {
        var diagnostics = input.first().split(",").map { it.toInt() }
        Helper.debugMsg("Initial state: $diagnostics")

        val allFish = simulatePopulation(diagnostics, _daysToWatchFirstPuzzle)
        Helper.debugMsg("There are $allFish fish alive after $_daysToWatchFirstPuzzle days.")

        return "$allFish"
    }

    override fun solveSecond(): String? {
        var diagnostics = input.first().split(",").map { it.toInt() }
        Helper.debugMsg("Initial state: $diagnostics")

        val allFish = simulatePopulation(diagnostics, _daysToWatchSecondPuzzle)
        Helper.debugMsg("There are $allFish fish alive after $_daysToWatchSecondPuzzle days.")

        return "$allFish"
    }

    private fun simulatePopulation(initialPopulation: List<Int>, runTime: Int): Long {
        val fishCountPerCycle = LongArray(9)
        initialPopulation.forEach { fishCountPerCycle[it]++ }

        repeat(runTime) {
            val nNewFish = fishCountPerCycle[0]
            for (i in _cycleRange) {
                fishCountPerCycle[i - 1] = fishCountPerCycle[i]
            }
            fishCountPerCycle[_cycle - 1] += nNewFish
            fishCountPerCycle[_newFishValue] = nNewFish
        }

        return fishCountPerCycle.sum()
    }
}

