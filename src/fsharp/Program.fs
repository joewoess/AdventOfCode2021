module main

open fsharp.helper.literals
open fsharp.helper.printing
open fsharp.helper.debug
open fsharp.helper.records
open fsharp.helper.reflection
open fsharp.helper.util

[<EntryPoint>]
let main (args: string []) : int =
    let IsDebug = args |> Array.contains "--debug"
    let IsTest = args |> Array.contains "--test"
    let OnlyFirst = args |> Array.contains "--first"
    let OnlySecond = args |> Array.contains "--second"

    let OnlyLast = args |> Array.contains "--last"

    let dayNumberParams =
        args
        |> Array.filter (fun it -> it.StartsWith("--") |> not)
        |> Array.map (fun it -> it |> int)
        |> Array.filter (fun it -> it |> betweenIncl (1, 25))
        |> Array.sort

    let OnlyNumbers = dayNumberParams |> Array.isEmpty |> not

    let allDays =
        { 1 .. MaxDays }
        |> Seq.toArray
        |> Array.map
            (fun idx ->
                { DayNumber = idx
                  Implementation = (GetImplForDay idx)
                  InputFile = GetInputFileForDay false idx
                  TestInputFile = GetInputFileForDay true idx })
    
    
    DebugMsg IsDebug $"Day01: %A{Array.head allDays}"

    let whatIsAvailable =
        allDays
        |> Array.filter
            (fun avail ->
                avail.Implementation.IsSome
                || avail.TestInputFile.IsSome
                || avail.InputFile.IsSome)

    let lastOption =
        (match whatIsAvailable |> Array.isEmpty with
         | true -> None
         | false -> Some(whatIsAvailable |> Array.last))


    let whatToDisplay =
        (match lastOption with
         | None -> Array.empty
         | Some last ->
             (match (OnlyLast, OnlyNumbers) with
              | true, true ->
                  whatIsAvailable
                  |> Array.filter (fun avail -> dayNumberParams |> Array.contains avail.DayNumber)
                  |> Array.append (Array.singleton last)
              | true, false -> Array.singleton last
              | false, true ->
                  whatIsAvailable
                  |> Array.filter (fun avail -> dayNumberParams |> Array.contains avail.DayNumber)
              | false, false -> whatIsAvailable))
        |> Array.sortBy (fun avail -> avail.DayNumber)
        |> Array.distinct

    PrintGreeting()

    DebugMsg IsDebug $"IsTest[%b{IsTest}] IsDebug[%b{IsDebug}] ShowLast[%b{OnlyLast}] OnlyFirst[%b{OnlyFirst}] OnlySecond[%b{OnlySecond}]"
    DebugMsg IsDebug $"DayNumbers =%A{dayNumberParams}"
    DebugMsg IsDebug $"Available  #=%A{MapToDayNumber whatIsAvailable}"
    DebugMsg IsDebug $"ToDisplay  #=%A{MapToDayNumber whatToDisplay}"

    if OnlyLast then
        printfn "Show last entry"

    if OnlyNumbers then
        printfn $"Show entries for days: %A{dayNumberParams}"

    PrintResultHeader()

    for available in whatToDisplay do
        PrintPuzzleSolution IsTest IsDebug available

    PrintSeparator()
    0
