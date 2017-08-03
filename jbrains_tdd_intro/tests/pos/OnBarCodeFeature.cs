using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Feature("OnBarCode")]
    public class OnBarCodeFeature
    {
        public void null_gives_invalid_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            System(fakeDisplay).OnBarCode(BarCode.Create(null));
            Check.That(() => fakeDisplay.LastMessage == "Invalid barcode");
        }

        private under_test.PointOfSaleSystem System(Display display)
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
