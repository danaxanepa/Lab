using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Feature("OnBarCode")]
    public class OnBarCodeFeature
    {
        [Row((string)null)]
        [Row("")]
        public void invalid_barcode(string value)
        {
            var fakeDisplay = new FakeDisplay();
            System(fakeDisplay).OnBarCode(BarCode.Create(value));
            Check.That(() => fakeDisplay.LastMessage == "Invalid barcode");
        }

        public void handle_unknown_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            System(fakeDisplay).OnBarCode(BarCode.Create("12345"));
            Check.That(() => fakeDisplay.LastMessage == "No price found");

        }

        private PointOfSaleSystem System(Display display)
        {
            return new PointOfSaleSystem(display);
        }

        private class FakeDisplay : Display
        {
            public string LastMessage;
            public void Print(string message)
            {
                LastMessage = message;
            }
        }    
    }
}
