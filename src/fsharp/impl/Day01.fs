namespace fsharp.impl

open fsharp.puzzle

type Day01() =
    inherit Puzzle()

    let countIncreases (collection: List<int>) : int =
        collection
        |> List.windowed 2
        |> List.filter (fun pair -> pair.[1] > pair.[0])
        |> List.length

    override this.SolveFirst(isDebug, inputPath) =
        this.inputLines isDebug inputPath
        |> Option.map
            (fun lines ->
                lines
                |> List.map (fun it -> it.Trim())
                |> List.map (fun it -> it |> int)
                |> countIncreases)
        |> Option.map (fun increases -> $"%i{increases}")
        |> Option.orElse None

    override this.SolveSecond(isDebug, inputPath) =
        this.inputLines isDebug inputPath
        |> Option.map
            (fun lines ->
                lines
                |> List.map (fun it -> it.Trim())
                |> List.map (fun it -> it |> int)
                |> List.windowed 3
                |> List.map List.sum
                |> countIncreases)
        |> Option.map (fun increases -> $"%i{increases}")
        |> Option.orElse None
