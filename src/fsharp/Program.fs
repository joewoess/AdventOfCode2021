module main

open System.IO
open fsharp.puzzle
open fsharp.Helper.literals
open fsharp.Helper.printing
open fsharp.Helper.reflection
open fsharp.Helper.util

[<EntryPoint>]
let main (args: string []) : int =
    let IsDebug = args |> Array.contains "--debug"
    let IsTest = args |> Array.contains "--test"

    let chosenInputPath =
        match IsTest with
        | true -> TestInputPath
        | false -> InputPath

    let files = inputFiles chosenInputPath
    let implementations = classes chosenInputPath

    //    let whatIsAvailable =
//        { idx in 1 .. MaxDays }
//        |> Seq.toArray
//        |> {DayNumber = idx; Implementation = implementations[idx]; InputPath = files[idx]}



    let dayNumbers =
        args
        |> Array.filter (fun it -> it.StartsWith("--") |> not)
        |> Array.map (fun it -> it |> int)
        |> Array.filter (fun it -> it |> betweenIncl (1, 25))
        |> Array.sort

    PrintGreeting()

    DebugMsg IsDebug $"Files #=%i{files.Length}"
    DebugMsg IsDebug $"Impl  #=%i{implementations.Length}"

    if args |> Array.contains "--last" then
        DebugMsg IsDebug "Only show last entry (cmd arg --last)"
        PrintResultHeader()
        PrintPuzzleSolution(Array.last implementations)
        PrintSeparator()

    let numberPicksOrAll =
        match dayNumbers |> Array.isEmpty with
        | false ->
            printfn $"Only show entries nr %A{dayNumbers}"
            dayNumbers
        | true -> ({ 1 .. MaxDays } |> Seq.toArray)

    PrintResultHeader()

    for day in numberPicksOrAll do
        PrintPuzzleSolution(implementations.[day])

    PrintSeparator()
    0
