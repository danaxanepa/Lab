namespace under_test

    type BarCode = 
        { Value: string }
        static member Create v = { Value = v }

        member this.IsEmpty = 
            System.String.IsNullOrEmpty(this.Value)

    type Display = 
        abstract member Print : string -> unit

    type Price = 
        { Value: Option<float> }

        static member Create value = { Value = Some value }
        static member Empty = { Value = None }

        member this.IsEmpty = this.Value.IsNone

        member this.ToString () = 
            match this.Value with
            | None -> System.String.Empty
            | Some a -> a.ToString()

    type PriceService =
        abstract member GetPrice : BarCode -> Price

    type PointOfSaleSystem (display: Display, prices: PriceService) =
        
        member this.OnBarCode (value: BarCode) = 
            let getMessage = 
                if (value.IsEmpty) then
                    "Invalid barcode"
                else
                    let price = prices.GetPrice value
                    match price.IsEmpty with
                    | true -> "No price found"
                    | false -> price.ToString()
            display.Print getMessage