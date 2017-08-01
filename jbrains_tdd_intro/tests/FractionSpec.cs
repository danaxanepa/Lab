using Cone;
using under_test;

namespace jbrains_tdd_intro.tests
{
    [Describe(typeof(under_test.Fraction))]
    public class FractionSpec
    {
        public void to_float()
        {
            Check.That(() => F(1, 2).ToFloat() == 0.5);
        }

        public void supports_equality()
        {
            Check.That(() => F(1, 2) == F(1, 2));
        }

        public void _0_plus_0()
        {
            Check.That(() => (F(0) + F(0)).ToFloat() == 0.0);
        }

        public void _1_plus_1()
        {
            Check.That(() => (F(1) + F(1)).ToFloat() == 2.0);
        }

        private Fraction F(int numerator, int denominator)
        {
            return Fraction.Create(numerator, denominator);
        }

        private Fraction F(int value)
        {
            return Fraction.Create(value);
        }
    }
}
