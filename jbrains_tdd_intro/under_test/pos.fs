namespace under_test

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

    type PointOfSaleSystem (display: Display, prices: PriceService) =
        
        let session = new List<Price>()

        member this.OnBarCode (value: BarCode) = 
            let getMessage = 
                let price = prices.GetPrice value
                match price.IsEmpty with
                | true -> sprintf "No price found for '%s'" (value.ToString())
                | false -> 
                    session.Add(price)
                    price.ToString()
            display.Print getMessage
     
        member this.OnTotal() = 
            let total = session |> Seq.sum
            display.Print (sprintf "Total: %s" <| total.ToString())
     
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