module public fsharp.helper.literals

[<Literal>]
let MaxDays: int = 25

[<Literal>]
let InputPath: string = "../../input/"

[<Literal>]
let TestInputPath: string = "../../testInput/"

[<Literal>]
let SeparatorChar: char = '-'

[<Literal>]
let NoImplMsg: string = "NO-IMPL"

[<Literal>]
let NoInputMsg: string = "NO-INPUT"

[<Literal>]
let NoImplOrInputMsg: string = "NONE"

[<Literal>]
let UnknownMsg: string = "UNKNOWN"

[<Literal>]
let GreetingMessage =
    """AdventOfCode Runner for 2021
Challenge at: https://adventofcode.com/2021/
Author: Johannes Wöß
Written in F# 6 / .NET 6"""
