namespace under_test

    type BarCode = 
        { Value: string }
        static member Create v = { Value = v }

    type Display = 
        abstract member Print : string -> unit

    type PointOfSaleSystem (display: Display) =
        
        member this.OnBarCode (value: BarCode) = 
            display.Print "Invalid barcode"
