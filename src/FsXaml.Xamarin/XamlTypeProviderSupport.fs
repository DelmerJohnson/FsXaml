namespace FsXaml

open System
open System.Collections.Generic
open System.Windows
open Xamarin.Forms
open Xamarin.Forms.Xaml

/// Provides access to named children of a VisualElement
type XamlFileAccessor(root : VisualElement) =
    let dict = new Dictionary<_,_>()
        
    /// Gets a named child element by name
    member __.GetChild name = 
        match dict.TryGetValue name with
        | true,element -> element
        | false,_ -> 
            let element = root.FindByName name
            dict.[name] <- element
            element

    /// The root element of the XAML document
    member __.Root = root
