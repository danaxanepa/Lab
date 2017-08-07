using Cone;

namespace jbrains_tdd_intro.tests.pos
{
    [Feature("OnTotalFeature")]
    public class OnTotalFeature
    {
        public void show_price_for_0_items()
        {
            var fakeDisplay = new FakeDisplay();
            var fakePriceService = new FakePriceService();
            Setup.System(fakeDisplay, fakePriceService).OnTotal();
            Check.That(() => fakeDisplay.LastMessage == "Total: 0");
        }
    }
}