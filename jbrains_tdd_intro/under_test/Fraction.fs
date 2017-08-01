﻿namespace under_test

    type Fraction =
        { Numerator: int; Denominator: int }

        static member Create (value) = 
            { Numerator = value; Denominator = 1 }
            
        static member Create (numerator, denominator) = 
            { Numerator = numerator; Denominator = denominator }

        member this.ToFloat() = 
            (float)this.Numerator / (float)this.Denominator

        static member op_Equality ((a: Fraction), (b: Fraction)) = a.Equals(b)
        static member op_Inequality ((a: Fraction), (b: Fraction)) = a.Equals(b) = false

        static member (+) ((a: Fraction), (b: Fraction)) = 
            Fraction.Create(a.Numerator + b.Numerator, a.Denominator)
