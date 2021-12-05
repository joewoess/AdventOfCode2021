namespace fsharp.impl

open fsharp.puzzle

type Day01(filename: string) =
    inherit Puzzle(filename: string)

    let countIncreases (collection: List<int>) : int =
        collection
        |> List.windowed 2
        |> List.filter (fun pair -> pair.[1] > pair.[0])
        |> List.length

    override this.SolveFirst =
        let increases =
            this.inputLines
            |> List.map (fun it -> it.Trim())
            |> List.map (fun it -> it |> int)
            |> countIncreases

        ($"%i{increases}" |> Option.Some)

    override this.SolveSecond =
        let increases =
            this.inputLines
            |> List.map (fun it -> it.Trim())
            |> List.map (fun it -> it |> int)
            |> List.windowed 3
            |> List.map (List.sum)
            |> countIncreases

        ($"%i{increases}" |> Option.Some)
