namespace FsXaml

open System
open Xamarin.Forms

[<AllowNullLiteral>]
type public IViewController =
    abstract member Attach : Element -> unit
    abstract member Detach : Element -> unit

