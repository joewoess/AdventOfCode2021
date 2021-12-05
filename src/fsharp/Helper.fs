module public fsharp.Helper

module public literals =
    [<Literal>]
    let MaxDays: int = 25

    [<Literal>]
    let InputPath: string = "../../input/"

    [<Literal>]
    let TestInputPath: string = "../../testInput/"

    [<Literal>]
    let SeparatorChar: char = '-'

    [<Literal>]
    let NoSolutionMsg: string = "NO-IMPL"

    [<Literal>]
    let NoInputMsg: string = "NO-INPUT"

    [<Literal>]
    let GreetingMessage =
        """AdventOfCode Runner for 2021
Challenge at: https://adventofcode.com/2021/
Author: Johannes Wöß
Written in F# 6 / .NET 6"""

module public util =
    open System.IO

    let inline PathFromFilename (inputPath: string) (filename: string) =
        Path.Combine(inputPath, $"{filename}.txt")

    let inline FirstCharUpper (s: string) =
        match s.Length with
        | 0 -> s
        | _ -> s.Substring(0, 1).ToUpper() + s.Substring(1)

    let inline betweenIncl (min, max) num = (num >= min) && (num <= max)

    let rec repeat (item, n) =
        seq {
            match n > 0 with
            | true ->
                yield item
                yield! repeat (item, n - 1)
            | false -> "" |> ignore
        }

module public printing =
    open System
    open literals
    open util
    open fsharp.puzzle

    let separator =
        [ (SeparatorChar, Console.WindowWidth - 1) ]
        |> Seq.collect repeat
        |> Array.ofSeq
        |> String

    let inline PrintSeparator unit = printfn $"%s{separator}"

    let PrintGreeting unit =
        PrintSeparator()
        printfn $"%s{GreetingMessage}"
        PrintSeparator()

    let inline PrintResultHeader unit =
        printfn "|  Day  |         1st |         2nd |"

    let DebugMsg isTest msg = if isTest then printfn msg

    let PrintPuzzleSolution (puzzle: Puzzle) : unit =
        let (first, second) = (puzzle.SolveFirst, puzzle.SolveSecond)

        let (firstStr, secondStr) =
            (match first with
             | Some (result) -> result
             | None -> NoSolutionMsg)
                .PadLeft 10,
            (match second with
             | Some (result) -> result
             | None -> NoSolutionMsg)
                .PadLeft 10

        printfn $"| %s{puzzle.GetType().Name} |  %s{firstStr} |  %s{secondStr} |"

module public reflection =
    open System.Reflection
    open System.IO
    open fsharp.puzzle
    open util

    type public Available =
        { DayNumber: int
          Implementation: Puzzle
          InputPath: string }

    let definedImplementations =
        Assembly.GetExecutingAssembly().DefinedTypes
        |> Array.ofSeq
        |> Array.filter (fun t -> t.IsSubclassOf(typeof<Puzzle>))
        |> Array.sortBy (fun t -> t.Name)

    let inputFiles chosenPath =
        let directory = DirectoryInfo(chosenPath)

        directory.GetFiles()
        |> Array.filter (fun file -> file.Extension = ".txt")
        |> Array.map (fun file -> file.Name)
        |> Array.map (fun file -> file.Replace(".txt", ""))
        |> Array.map (fun filename -> $"%s{FirstCharUpper filename}", PathFromFilename chosenPath filename)

    let classes chosenPath =
        definedImplementations
        |> Array.map
            (fun t ->
                t.GetConstructor(Array.singleton (typeof<string>)),
                (inputFiles chosenPath)
                |> Array.find (fun (name, path) -> name = t.Name)
                |> snd)
        |> Array.map (fun (ci, p) -> ci.Invoke(Array.singleton p))
        |> Array.map (fun obj -> obj :?> Puzzle)
