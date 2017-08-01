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

        private Fraction F(int nunerator, int denominator)
        {
            return Fraction.Create(nunerator, denominator);
        }
    }
}
