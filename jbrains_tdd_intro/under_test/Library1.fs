namespace under_test

    type Fraction =
        { Numerator: int; Denominator: int }

        static member Create (numerator, denominator) = 
            { Numerator = numerator; Denominator = denominator }

        member this.ToFloat() = 
            (float)this.Numerator / (float)this.Denominator
