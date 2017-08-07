using Cone;
using under_test;

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

        public void show_price_for_1_item()
        {
            var fakeDisplay = new FakeDisplay();
            var code = BarCode.Create("12345");
            var price = Price.Create(2.5);
            var fakePriceService = new FakePriceService() { { code, price } };

            var system = Setup.System(fakeDisplay, fakePriceService);
            system.OnBarCode(code);
            system.OnTotal();

            var expected = $"Total: {price}";
            Check.That(() => fakeDisplay.LastMessage == expected);
        }
    }
}