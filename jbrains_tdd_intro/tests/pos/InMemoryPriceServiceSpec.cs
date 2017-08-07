using System;
using Cone;
using Microsoft.FSharp.Control;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Describe(typeof(InMemoryPriceService))]
    public class InMemoryPriceServiceSpec
    {
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

        private static PriceService Service(Action<BarCode> notification)
        {
            var service = new InMemoryPriceService();
            service.PriceNotFound += (sender, args) => notification(args.BarCode);
            return service;
        }
    }
}