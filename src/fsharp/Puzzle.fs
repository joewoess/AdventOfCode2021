namespace fsharp.puzzle

open System.IO

[<AbstractClass>]
type public Puzzle(filename: string) =
    member private this.input =
        lazy (File.ReadAllLines(filename) |> List.ofArray)

    member this.inputLines =
        (match this.input.IsValueCreated with
         | true -> this.input.Value
         | false -> this.input.Force())

    abstract member SolveFirst : Option<string>
    abstract member SolveSecond : Option<string>
