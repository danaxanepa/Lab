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
        static member Empty = { Value = None }

        member this.IsEmpty = this.Value.IsNone

        override this.ToString () = 
            match this.Value with
            | None -> System.String.Empty
            | Some a -> a.ToString()

    type PriceService =
        abstract member GetPrice : BarCode -> Price

    type PointOfSaleSystem (display: Display, prices: PriceService) =
        
        member this.OnBarCode (value: BarCode) = 
            let getMessage = 
                let price = prices.GetPrice value
                match price.IsEmpty with
                | true -> sprintf "No price found for '%s'" (value.ToString())
                | false -> price.ToString()
            display.Print getMessage
     
     
     type PriceNotFoundEvent (code: BarCode) = 
        inherit System.EventArgs()
        member this.BarCode = code
     
     type InMemoryPriceService (values : seq<BarCode * Price>) = 
        let priceNotFound = new Event<PriceNotFoundEvent>()

        member this.prices = values |> Map.ofSeq

        [<CLIEvent>]
        member this.PriceNotFound = priceNotFound.Publish
        
        interface PriceService with
            member this.GetPrice barCode = 
                match this.prices.TryFind barCode with
                | Some p -> p
                | _ -> 
                    priceNotFound.Trigger (new PriceNotFoundEvent(barCode))
                    Price.Empty