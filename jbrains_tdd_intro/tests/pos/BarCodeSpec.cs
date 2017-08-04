using System;
using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Describe(typeof(BarCode))]
    public class BarCodeSpec
    {
        public void does_not_accept_null()
        {
            Check.Exception<ArgumentException>(() => BarCode.Create(null));
        }

        public void does_not_accept_empty()
        {
            Check.Exception<ArgumentException>(() => BarCode.Create(string.Empty));
        }
    }
}