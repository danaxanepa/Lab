using System;
using Cone;

namespace jbrains_tdd_intro.tests
{
    [Feature("GCD")]
    public class GCDSpec
    {
        [Row(0, 0, 0)]
        [Row(0, 1, 1)]
        [Row(1, 0, 1)]
        [Row(1, 2, 1)]
        [Row(3, 5, 1)]
        [Row(2, 4, 2)]
        [Row(5, 15, 5)]
        [Row(15, 18, 3)]
        public void check(int a, int b, int expected)
        {
            Check.That(() => under_test.Math.gcd(a, b) == expected);
        }
    }
}
