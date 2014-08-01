namespace FsXaml

open System
open System.IO
open System.Reflection
open System.Text.RegularExpressions
open System.Windows
open Xamarin.Forms
open Xamarin.Forms.Xaml

module LoadXaml =
    let createNew<'a when 'a : (new : unit->'a) and 'a :> VisualElement> () =
        let result = new 'a()
        let output = Xamarin.Forms.Xaml.Extensions.LoadFromXaml(result, typeof<'a>)
        output

    let into<'a when 'a :> VisualElement> (element : 'a) =        
        Xamarin.Forms.Xaml.Extensions.LoadFromXaml(element, typeof<'a>) 
        |> ignore

    let source<'a when 'a : (new : unit->'a) and 'a :> VisualElement> () =
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<'a>).Assembly

        let fullName = typeof<'a>.FullName
        let regexSearch = sprintf "x:Class *= *\"%s\"" fullName
        let inputSearch = sprintf "x:Class=\"%s\"" fullName
        let regex = Regex(regexSearch, RegexOptions.ECMAScript)

        let getSource resourceName =
            use stream = assembly.GetManifestResourceStream(resourceName)
            use reader = new StreamReader(stream)
            let input = reader.ReadToEnd()
            match regex.IsMatch(input), input.Contains(inputSearch) with
            | false, false -> None
            | _,_ -> Some input
            
        let source = 
            assembly.GetManifestResourceNames()
            |> Seq.choose getSource
            |> Seq.head

        source              