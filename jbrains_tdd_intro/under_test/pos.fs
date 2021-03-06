﻿namespace under_test

    open System
    open System.Linq
    open System.Collections.Generic

    type BarCode = 
        { Value: string }
        static member Create v = 
            if (System.String.IsNullOrEmpty(v)) then
                invalidArg "v" "BarCode cannot be empty"
            else
                { Value = v }

        override this.ToString () = 
            this.Value

    type Display = 
        abstract member Print : string -> unit

    type Price = 
        { Value: Option<float> }

        static member Create value = { Value = Some value }
        static member Zero = Price.Create 0.0
        static member Empty = { Value = None }

        static member (+) (a, b) = 
            match (a.Value, b.Value) with
            | (None, None) -> Price.Empty
            | (None, Some b') -> b
            | (Some a', None) -> a
            | (Some a', Some b') -> Price.Create(a' + b')

        member this.IsEmpty = this.Value.IsNone

        override this.ToString () = 
            match this.Value with
            | None -> System.String.Empty
            | Some a -> a.ToString()

    type PriceNotFoundEventArgs (code: BarCode) = 
        inherit System.EventArgs()
        member this.BarCode = code

    type MyDelegate = delegate of obj * PriceNotFoundEventArgs -> unit

    type PriceService =
        abstract member GetPrice : BarCode -> Price
        [<CLIEvent>]
        abstract member PriceNotFound : IEvent<PriceNotFoundEventArgs>

    type ShoppingSession () = 
        let session = new List<Price>()
        let missingPrices = new List<BarCode>()

        member this.RegisterMissingPrice barCode = missingPrices.Add barCode
        member this.RegisterPrice price = session.Add price
        member this.MissingPrices () = missingPrices
        member this.Prices() = session


    type PointOfSaleSystem (display: Display, prices: PriceService) =
        let session = new ShoppingSession()
        do
            prices.PriceNotFound.AddHandler (fun sender e -> session.RegisterMissingPrice e.BarCode)
        
        member this.display = display
        member this.prices = prices
        
        member this.OnBarCode (value: BarCode) = 
            let getMessage = 
                let price = prices.GetPrice value
                match price.IsEmpty with
                | true -> sprintf "No price found for '%s'" (value.ToString())
                | false -> 
                    session.RegisterPrice price
                    price.ToString()
            display.Print getMessage
     
        member this.OnTotal([<ParamArray>] manualPrices : (BarCode * Price) array) = 
            let manualPricesMap = manualPrices |> Map.ofSeq
            let manulPricesFound = session.MissingPrices() |> Seq.map manualPricesMap.TryFind |> Seq.filter Option.isSome |> Seq.map Option.get
            let total = session.Prices() |> Seq.append manulPricesFound |> Seq.sum
            let reallyMissingPrices = session.MissingPrices() |> Seq.filter (fun x -> (manualPricesMap.ContainsKey x) = false) |> Seq.toList
            
            let missingPricesMessage = 
                match reallyMissingPrices with
                | [] -> ""
                | x -> " No price for " + System.String.Join(", ", x)

            display.Print (sprintf "Total: %s%s" (total.ToString()) missingPricesMessage)
     
     type InMemoryPriceService (values : seq<BarCode * Price>) = 
        let priceNotFound = new Event<PriceNotFoundEventArgs>()

        member this.prices = values |> Map.ofSeq
        
        interface PriceService with
            [<CLIEvent>]
            member this.PriceNotFound = priceNotFound.Publish
        
            member this.GetPrice barCode = 
                match this.prices.TryFind barCode with
                | Some p -> p
                | _ -> 
                    priceNotFound.Trigger (new PriceNotFoundEventArgs(barCode))
                    Price.Empty