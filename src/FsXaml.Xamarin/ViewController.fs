namespace FsXaml

open System
open System.Windows
open Xamarin.Forms


type public ViewController() =
    static let customChanged (bindable:BindableObject) (oldValue:obj) (newValue:obj) = 
        let fe = Utilities.castAs<VisualElement> newValue
        let behaviorType = Utilities.castAs<Type> newValue
        if fe <> null && behaviorType <> null then
            let controller = Utilities.castAs<IViewController> <| Activator.CreateInstance behaviorType
            if controller <> null then
                fe.Focused.Add(fun a -> controller.Attach fe)
                fe.Unfocused.Add(fun a -> controller.Detach fe)

    static let changed : BindableProperty.BindingPropertyChangedDelegate = new BindableProperty.BindingPropertyChangedDelegate(customChanged)

    static let CustomProperty : BindableProperty = BindableProperty.CreateAttached("Custom", typeof<Type>, typeof<ViewController>, null, defaultBindingMode = BindingMode.OneWay, propertyChanged = changed)

    static member SetCustom(bindable : BindableObject, behaviorType: Type) =
        bindable.SetValue(CustomProperty, behaviorType)

    static member GetCustom(bindable: BindableObject) =
        bindable.GetValue(CustomProperty) :?> Type




