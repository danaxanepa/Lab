using System;
using Cone;
using under_test;

namespace jbrains_tdd_intro.tests.pos
{
    [Describe(typeof(InMemoryPriceService))]
    public class InMemoryPriceServiceSpec
    {
        [Pending]
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
            
            return service;
        }
    }
}