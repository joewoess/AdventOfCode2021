module fsharp.helper.debug

let DebugMsg isTest msg = if isTest then printfn msg
