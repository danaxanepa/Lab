using Cone;
using under_test;

namespace jbrains_tdd_intro.tests
{
    [Describe(typeof(under_test.Fraction))]
    public class FractionSpec
    {
        public void to_float()
        {
            Check.That(() => Fraction.Create(1, 2).ToFloat() == 0.5);
        }
    }
}
