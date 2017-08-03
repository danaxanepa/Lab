namespace under_test

    type BarCode = 
        { Value: string }
        static member Create v = { Value = v }

        member this.IsEmpty = 
            System.String.IsNullOrEmpty(this.Value)

    type Display = 
        abstract member Print : string -> unit

    type PointOfSaleSystem (display: Display) =
        
        member this.OnBarCode (value: BarCode) = 
            if (value.IsEmpty) then
                display.Print "Invalid barcode"
            else
                display.Print "No price found"
