# Advent of Code 2021

My solutions to the coding challenge [adventofcode](https://adventofcode.com/2021) written in different kind of languages :)

Code automatically takes input files day01.txt, day02.txt, etc
All implemented solutions will be linked in the [Challenges table](##Challenges)  with their respective languages.

---

## Challenges

| Day | Challenge | C# | F# | Kotlin | Rust | Python |
| ---: |:---------| :-------:| :-------:| :-------:| :-------:| :-------:|
|  1  | [Sonar Sweep](https://adventofcode.com/2021/day/1) | [Csharp](src/csharp/impl/Day01.cs) | [FSharp](src/fsharp/impl/Day01.fs) | [Kotlin](src/kotlin/app/adventofcode/impl/Day01.kt) | Rust | Python
|  2  | [Dive!](https://adventofcode.com/2021/day/2) | [Csharp](src/csharp/impl/Day02.cs) | FSharp | [Kotlin](src/kotlin/app/adventofcode/impl/Day02.kt) | Rust | Python
|  3  | [Binary Diagnostic](https://adventofcode.com/2021/day/3) | [Csharp](src/csharp/impl/Day03.cs) | FSharp | [Kotlin](src/kotlin/app/adventofcode/impl/Day03.kt) | Rust | Python
|  4  | [Giant Squid](https://adventofcode.com/2021/day/4) | [Csharp](src/csharp/impl/Day04.cs) | FSharp | Kotlin | Rust | Python
|  5  | [Hydrothermal Venture](https://adventofcode.com/2021/day/5) | [Csharp](src/csharp/impl/Day05.cs) | FSharp | Kotlin | Rust | Python
|  6  | [Lanternfish](https://adventofcode.com/2021/day/6) | Csharp | FSharp | Kotlin | Rust | Python
|  7  | [The Treachery of Whales](https://adventofcode.com/2021/day/7) | [Csharp](src/csharp/impl/Day07.cs) | FSharp | Kotlin | Rust | Python
|  8  | [Seven Segment Search](https://adventofcode.com/2021/day/8) | [Csharp](src/csharp/impl/Day08.cs) | FSharp | Kotlin | Rust | Python
|  9  | [Challenge 09](https://adventofcode.com/2021/day/9) | Csharp | FSharp | Kotlin | Rust | Python
| 10  | [Challenge 10](https://adventofcode.com/2021/day/10) | Csharp | FSharp | Kotlin | Rust | Python
| 11  | [Challenge 11](https://adventofcode.com/2021/day/11) | Csharp | FSharp | Kotlin | Rust | Python
| 12  | [Challenge 12](https://adventofcode.com/2021/day/12) | Csharp | FSharp | Kotlin | Rust | Python
| 13  | [Challenge 13](https://adventofcode.com/2021/day/13) | Csharp | FSharp | Kotlin | Rust | Python
| 14  | [Challenge 14](https://adventofcode.com/2021/day/14) | Csharp | FSharp | Kotlin | Rust | Python
| 15  | [Challenge 15](https://adventofcode.com/2021/day/15) | Csharp | FSharp | Kotlin | Rust | Python
| 16  | [Challenge 16](https://adventofcode.com/2021/day/16) | Csharp | FSharp | Kotlin | Rust | Python
| 17  | [Challenge 17](https://adventofcode.com/2021/day/17) | Csharp | FSharp | Kotlin | Rust | Python
| 18  | [Challenge 18](https://adventofcode.com/2021/day/18) | Csharp | FSharp | Kotlin | Rust | Python
| 19  | [Challenge 19](https://adventofcode.com/2021/day/19) | Csharp | FSharp | Kotlin | Rust | Python
| 20  | [Challenge 20](https://adventofcode.com/2021/day/20) | Csharp | FSharp | Kotlin | Rust | Python
| 21  | [Challenge 21](https://adventofcode.com/2021/day/21) | Csharp | FSharp | Kotlin | Rust | Python
| 22  | [Challenge 22](https://adventofcode.com/2021/day/22) | Csharp | FSharp | Kotlin | Rust | Python
| 23  | [Challenge 23](https://adventofcode.com/2021/day/23) | Csharp | FSharp | Kotlin | Rust | Python
| 24  | [Challenge 24](https://adventofcode.com/2021/day/24) | Csharp | FSharp | Kotlin | Rust | Python
| 25  | [Challenge 25](https://adventofcode.com/2021/day/25) | Csharp | FSharp | Kotlin | Rust | Python

---

## Usage

Depending on the language version, all that is need is to go into the respective folder and
use their common build tool to run it.

```
# these are the valid optional parameters for all implementations (can be freely combined)
 --test      ... Use test input instead of puzzle input
 --debug     ... Show debug output
 --last      ... Show last challenge commited
 01 4 20     ... Number list specifying certain days to output 
```
```zsh
# using gradle for kotlin
# or for the included gradle wrapper use gradlew (or ./gradlew on windows)
gradle run --args='01'
gradlew run --args='01'

# using dotnet for c# and f#
dotnet run -- --test

# using python3 for rust
cargo run -- 01

# using python3 for python
python3 main.py --debug
```

## Languages used in this challenge

* C# 10
* Kotlin 1.6.0
* Rust 1.56.1
* F# 6
* Python 3.10
* ...

## Folder structure 

```
+---input
|   - day01.txt
|   - day02.txt
|   - ...
+---src
|   +---csharp
|   |   - csharp.csproj
|   +---fsharp
|   |   - fsharp.fsproj
|   +---kotlin
|   |   - settings.gradle.kts
|   |   +---app
|   |   |   +---adventofcode
|   +---python
|   +---rust
+---testInput
|   - day01.txt
|   - day02.txt
|   - ...
- README.md
```


## Sample output

```log
------------------------------------------------------------------------------
AdventOfCode Runner for 2021
Challenge at: https://adventofcode.com/2021/
Author: Johannes Wöß
Written in C# 10 / .NET 6
------------------------------------------------------------------------------
|  Day  |         1st |         2nd |
| Day01 |        1624 |        1653 |
Could not find solution for day Day02
```
