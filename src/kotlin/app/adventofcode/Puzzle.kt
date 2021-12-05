package adventofcode

import java.io.File

abstract class Puzzle(filename: String) {
    protected val input: List<String> by lazy {
        File(filename).readLines(Charsets.UTF_8)
    }

    abstract fun solveFirst(): String?
    abstract fun solveSecond(): String?
}