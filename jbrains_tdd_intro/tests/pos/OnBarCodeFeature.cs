using System;
using System.Collections;
using System.Collections.Generic;
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

    [Feature("OnBarCode")]
    public class OnBarCodeFeature
    {
        public void handle_unknown_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            System(fakeDisplay).OnBarCode(BarCode.Create("12345"));
            Check.That(() => fakeDisplay.LastMessage == "No price found");
        }

        public void show_price_for_known_barcode()
        {
            var fakeDisplay = new FakeDisplay();
            var code = BarCode.Create("12345");
            var price = Price.Create(2.5);
            var fakePriceService = new FakePriceService() { { code, price } };
            System(fakeDisplay, fakePriceService).OnBarCode(code);
            Check.That(() => fakeDisplay.LastMessage == price.ToString());
        }

        private PointOfSaleSystem System(Display display, PriceService prices = null)
        {
            return new PointOfSaleSystem(display, prices ?? new FakePriceService());
        }

        private class FakePriceService : PriceService, IEnumerable<KeyValuePair<BarCode, Price>>
        {
            Dictionary<BarCode, Price> prices = new Dictionary<BarCode, Price>();

            public void Add(BarCode code, Price price) => prices.Add(code, price);

            public Price GetPrice(BarCode code)
            {
                Price price;
                if (prices.TryGetValue(code, out price))
                    return price;
                return Price.Empty;
            }

            public IEnumerator<KeyValuePair<BarCode, Price>> GetEnumerator() => prices.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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
