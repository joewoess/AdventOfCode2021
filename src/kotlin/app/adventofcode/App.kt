package adventofcode

import java.io.File
import java.nio.file.Paths
import kotlin.reflect.KClass

private const val CONSOLE_WIDTH = 80
private const val NO_SOLUTION_MESSAGE = "NONE"
private const val INPUT_PATH = "../../../input/"
private const val TEST_INPUT_PATH = "../../../testInput/"

/** Helper functions to facilitate the logic in main() */
object Helper {
    var IsDebug = false
    var IsTest = false

    /** Prints the beginning header of console output */
    fun printGreeting() {
        printSeparator()
        println("AdventOfCode Runner for 2021")
        println("Challenge at: http://adventofcode.com/2021/")
        println("Author: Johannes Wöß")
        println("Written in Kotlin ${KotlinVersion.CURRENT}")
        printSeparator()
    }

    /** Prints a separator line spanning the console width */
    fun printSeparator() {
        println("-".repeat(CONSOLE_WIDTH))
    }

    /** Prints a the header for the result table */
    fun printResultHeader() {
        println("|  Day  |         1st |         2nd |")
    }

    /** Prints a message only if the global variable IsDebug is set */
    fun debugMsg(msg: String) {
        if (IsDebug) {
            println(msg)
        }
    }

    /** Prints the solution to a days puzzle */
    fun printPuzzleSolution(puzzle: Puzzle) {
        println("| ${puzzle::class.simpleName} |  ${(puzzle.solveFirst() ?: NO_SOLUTION_MESSAGE).padStart(10)} |  ${(puzzle.solveSecond() ?: NO_SOLUTION_MESSAGE).padStart(10)} |")
    }

    /** Find all Implementations of puzzles */
    fun getImplementationClasses(): List<Puzzle> {
        var classes = mutableListOf<Puzzle>()
        val puzzlesFiles = Paths.get(if (IsTest) TEST_INPUT_PATH else INPUT_PATH).toFile().walk().sorted()
            .filter { it.name.endsWith(".txt") }
        for (file in puzzlesFiles) {
            val clazz = try {
                val className = file.nameWithoutExtension.replaceFirstChar { it.uppercase() }
                debugMsg(className)
                Class.forName("adventofcode.impl.${className}").kotlin
            } catch (exc: ClassNotFoundException) {
                continue
            }
            val clazzConstructor = clazz.constructors.first { it.parameters.isNotEmpty() }
            val arguments = clazzConstructor.parameters.map { it.type.classifier as KClass<*> }.map { file.absolutePath }.toTypedArray()
            (clazzConstructor.call(*arguments) as Puzzle?)?.let { classes += it }
        }
        return classes
    }
}

fun main(args: Array<String>) {
    Helper.printGreeting()

    args.forEach { Helper.debugMsg(it) }
    Helper.IsTest = args.contains("--test")
    Helper.IsDebug = args.contains("--debug")

    Helper.debugMsg("IsTest: ${Helper.IsTest}")
    Helper.debugMsg("IsDebug: ${Helper.IsDebug}")

    // debug show current dir
    Helper.debugMsg(File("./").absolutePath)

    val implementations = Helper.getImplementationClasses()
    Helper.debugMsg("Implementations found: ${implementations.size}")

    if (args.contains("--last")) {
        println("Only show last entry (cmd arg --last)")
        Helper.printResultHeader()
        implementations.last().let {
            Helper.printPuzzleSolution(it)
        }
        Helper.printSeparator()
        return
    }
    val numberArgs = args.sorted().filter { !it.startsWith("--") }.map { it.toIntOrNull() ?: 0 }.filter { it != 0 }
    if (numberArgs.isNotEmpty()) {
        println("Only show entries nr $numberArgs")
        Helper.printResultHeader()
        numberArgs.filter { it in 1..25 }.forEach {
            implementations[it - 1].let { puzzle -> Helper.printPuzzleSolution(puzzle) }
        }
        Helper.printSeparator()
        return
    }

    Helper.printResultHeader()
    implementations.forEach { Helper.printPuzzleSolution(it) }
    Helper.printSeparator()
}