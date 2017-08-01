using Cone;
using under_test;

namespace jbrains_tdd_intro.tests
{
    [Describe(typeof(under_test.Class1))]
    public class Class1Spec
    {
        public void x_is_f_sharp()
        {
            Check.That(() => new Class1().X == "F#");
        }
    }
}
