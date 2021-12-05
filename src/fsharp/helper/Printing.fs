module public fsharp.helper.printing

open System
open fsharp.helper.literals
open fsharp.helper.util
open fsharp.helper.records

let separator =
    [ (SeparatorChar, Console.WindowWidth - 1) ]
    |> Seq.collect repeat
    |> Array.ofSeq
    |> String

let inline PrintSeparator _ = printfn $"%s{separator}"

let PrintGreeting _ =
    PrintSeparator()
    printfn $"%s{GreetingMessage}"
    PrintSeparator()

let inline PrintResultHeader _ =
    printfn "|  Day  |         1st |         2nd |"

let PrintPuzzleSolution isTest isDebug (available: Available) : unit =
    let first, second =
        match available with
        | { Implementation = Some impl
            InputFile = Some path } when isTest |> not -> (defaultArg (impl.SolveFirst(isDebug, path)) UnknownMsg, defaultArg (impl.SolveSecond(isDebug, path)) UnknownMsg)
        | { Implementation = Some impl
            TestInputFile = Some path } when isTest -> (defaultArg (impl.SolveFirst(isDebug, path)) UnknownMsg, defaultArg (impl.SolveSecond(isDebug, path)) UnknownMsg)
        | { Implementation = None
            InputFile = Some _ } when isTest |> not -> (NoImplMsg, NoImplMsg)
        | { Implementation = None
            TestInputFile = Some _ } when isTest -> (NoImplMsg, NoImplMsg)
        | { Implementation = Some _ } -> (NoInputMsg, NoInputMsg)
        | _ -> (NoImplOrInputMsg, NoImplOrInputMsg)

    printfn $"| Day%02i{available.DayNumber} |  %s{first.PadLeft(10)} |  %s{second.PadLeft(10)} |"
