namespace fsharp.impl

open System
open System.Collections.Generic
open fsharp.helper
open fsharp.puzzle
open fsharp.helper.debug

type Day06() =
    inherit Puzzle()

    [<Literal>]
    let DaysFirstPuzzle = 80

    [<Literal>]
    let DaysSecondPuzzle = 256

    [<Literal>]
    let Cycle = 7

    [<Literal>]
    let Offset = 2

    let countFishes (fishes: int list, days: int) : int64 =
        let allFishes: Int64 array =
            Array.init (Cycle + Offset) (fun _ -> 0L)

        fishes
        |> List.iter (fun fish -> allFishes.[fish] <- allFishes.[fish] + 1L)

        for day = 1 to days do
            let nextFishes: Int64 = allFishes.[0]

            for cycleIdx = 1 to (Cycle + Offset - 1) do
                allFishes.[cycleIdx - 1] <- allFishes.[cycleIdx]

            allFishes.[Cycle - 1] <- allFishes.[Cycle - 1] + nextFishes
            allFishes.[Cycle + Offset - 1] <- nextFishes

        allFishes |> Array.sum

    override this.SolveFirst(isDebug, inputPath) =
        let fishes =
            this.inputLines isDebug inputPath
            |> Option.map
                (fun lines ->
                    lines
                    |> List.head
                    |> (fun line -> line.Split(","))
                    |> Array.map (fun it -> it |> int)
                    |> List.ofArray)

        DebugMsg isDebug $"Fishes: %A{fishes}"

        fishes
        |> Option.map (fun fish -> countFishes (fish, DaysFirstPuzzle))
        |> Option.map (fun fishes -> $"%i{fishes}")
        |> Option.orElse None

    override this.SolveSecond(isDebug, inputPath) =
        let fishes =
            this.inputLines isDebug inputPath
            |> Option.map
                (fun lines ->
                    lines
                    |> List.head
                    |> (fun line -> line.Split(","))
                    |> Array.map (fun it -> it |> int)
                    |> List.ofArray)

        DebugMsg isDebug $"Fishes: %A{fishes}"

        fishes
        |> Option.map (fun fish -> countFishes (fish, DaysSecondPuzzle))
        |> Option.map (fun fishes -> $"%i{fishes}")
        |> Option.orElse None
