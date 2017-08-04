using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    public static class Setup
    {
        public static PointOfSaleSystem System(Display display  = null, PriceService prices = null)
        {
            return new PointOfSaleSystem(display ?? new FakeDisplay(), prices ?? new FakePriceService());
        }
    }
}