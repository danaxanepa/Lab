using Cone;
using under_test;

namespace jbrains_tdd_intro.tests
{
    [Describe(typeof(under_test.Fraction))]
    public class FractionSpec
    {
        public void supports_equality()
        {
            Check.That(() => F(1, 2) == F(1, 2));
        }

        public void to_string()
        {
            Check.That(() => F(1, 2).ToString() == "1/2");
        }

        public void _0_plus_0()
        {
            Check.That(() => (F(0) + F(0)) == F(0));
        }

        public void _1_plus_1()
        {
            Check.That(() => (F(1) + F(1)) == F(2));
        }

        public void _1_plus_2()
        {
            var result = F(1) + F(2);
            Check.That(() => result == F(3));
        }

        public void _1_plus_1by2()
        {
            var result = F(1) + F(1, 2);
            Check.That(() => result == F(3, 2));
        }

        public void _7by3_plus_4by5()
        {
            var result = F(7, 3) + F(4, 5);
            Check.That(
                () => result.Numerator == 47,
                () => result.Denominator == 15);
        }

        public void _4by2_plus_4by2()
        {
            var result = F(4, 2) + F(4, 2);
            Check.That(() => result == F(4, 1));
        }

        public void _2by2_plus_1by2()
        {
            var result = F(2, 2) + F(1, 2);
            Check.That(
                () => result == F(3, 2));
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
