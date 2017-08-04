using System.Collections;
using System.Collections.Generic;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    internal class FakePriceService : PriceService, IEnumerable<KeyValuePair<BarCode, Price>>
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
}