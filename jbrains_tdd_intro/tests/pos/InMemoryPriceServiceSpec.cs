using System;
using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Describe(typeof(InMemoryPriceService))]
    public class InMemoryPriceServiceSpec
    {
        public void gives_price_given_barcode()
        {
            var bc = BarCode.Create("123");
            var price = Price.Create(0.5);
            var result = Service(_ => {}, Tuple.Create(bc, price)).GetPrice(bc);
            Check.That(() => result == price);
        }

        public void should_notify_on_price_not_found()
        {
            BarCode notFound = null;
            Service(bc =>
            {
                notFound = bc;
            }).GetPrice(BarCode.Create("123"));
            Check.That(
                () => notFound != null, 
                () => notFound == BarCode.Create("123"));
        }

        private static PriceService Service(Action<BarCode> notification, params Tuple<BarCode, Price>[] prices)
        {
            PriceService service = new InMemoryPriceService(prices);
            service.PriceNotFound += (sender, args) => notification(args.BarCode);
            return service;
        }
    }
}