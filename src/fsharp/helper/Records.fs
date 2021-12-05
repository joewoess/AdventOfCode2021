module public fsharp.helper.records

open fsharp.puzzle

type public Available =
    { DayNumber: int
      Implementation: Option<Puzzle>
      InputFile: Option<string>
      TestInputFile: Option<string> }
