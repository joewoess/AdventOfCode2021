﻿module fsharp.helper.debug

let DebugMsg isDebug msg = if isDebug then printfn msg
