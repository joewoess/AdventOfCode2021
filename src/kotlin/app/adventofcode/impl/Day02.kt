package adventofcode.impl

import adventofcode.Puzzle
import adventofcode.Helper

class Day02(filename: String) : Puzzle(filename) {

    override fun solveFirst(): String? {
        val commands = input
            .map { it.split(" ") }
            .map { it[0].to(it[1].toInt()) }

        var depth = 0;
        var position = 0;
        for ((direction, amount) in commands) {
            when (direction) {
                "up" -> depth -= amount
                "down" -> depth += amount
                "forward" -> position += amount
                else -> Helper.debugMsg("Unknown direction ${direction}")
            }
        }

        Helper.debugMsg("Depth: $depth, Position: $position")
        return "${depth * position}"
    }

    override fun solveSecond(): String? {
        val commands = input
            .map { it.split(" ") }
            .map { it[0].to(it[1].toInt()) }

        var depth = 0;
        var position = 0;
        var aim = 0;

        for ((direction, amount) in commands) {
            when (direction) {
                "up" -> aim -= amount
                "down" -> aim += amount
                "forward" -> {
                    position += amount
                    depth += aim * amount
                }
                else -> Helper.debugMsg("Unknown direction ${direction}")
            }
        }

        Helper.debugMsg("Depth: $depth, Position: $position")
        return "${depth * position}"
    }
}
