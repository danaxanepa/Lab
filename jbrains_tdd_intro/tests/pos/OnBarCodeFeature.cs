using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Feature("OnBarCodeFeature")]
    public class OnBarCodeFeature
    {
        public void handle_unknown_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            Setup.System(fakeDisplay).OnBarCode(BarCode.Create("12345"));
            Check.That(() => fakeDisplay.LastMessage == "No price found for '12345'");
        }

        public void show_price_for_known_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            var code = BarCode.Create("12345");
            var price = Price.Create(2.5);
            var fakePriceService = new FakePriceService() { { code, price } };
            Setup.System(fakeDisplay, fakePriceService).OnBarCode(code);
            Check.That(() => fakeDisplay.LastMessage == price.ToString());
        }
    }
}
