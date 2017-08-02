namespace under_test

    module Math =
        let rec gcd a b = 
            match b with
            | 0 -> a
            | _ -> gcd b (a % b)

    type Fraction =
        { Numerator: int; Denominator: int }

        static member Create (value) = 
            { Numerator = value; Denominator = 1 }
            
        static member Create (numerator, denominator) = 
            { Numerator = numerator; Denominator = denominator }

        static member op_Equality ((a: Fraction), (b: Fraction)) = a.Equals(b)
        static member op_Inequality ((a: Fraction), (b: Fraction)) = a.Equals(b) = false

        override this.ToString() = 
            sprintf "%i/%i" this.Numerator this.Denominator

        static member (+) ((a: Fraction), (b: Fraction)) = 
            let numerator = a.Numerator * b.Denominator + a.Denominator * b.Numerator
            let denominator = a.Denominator * b.Denominator
            
            Fraction.Create(numerator, denominator).Reduce()

        member this.Reduce() = 
            let gcd = Math.gcd this.Numerator this.Denominator
            Fraction.Create(this.Numerator / gcd, this.Denominator / gcd)