﻿using System;
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

            Check.That(() => fakeDisplay.LastMessage == $"Total: {price}");
        }

        public void show_total_price_for_multiple_items()
        {
            var fakeDisplay = new FakeDisplay();
            var code1 = BarCode.Create("123");
            var code2 = BarCode.Create("456");
            var fakePriceService = new FakePriceService()
            {
                { code1, Price.Create(1) },
                { code2, Price.Create(2) }
            };

            var system = Setup.System(fakeDisplay, fakePriceService);
            system.OnBarCode(code1);
            system.OnBarCode(code2);
            system.OnTotal();

            Check.That(() => fakeDisplay.LastMessage == $"Total: 3");
        }

        public void show_total_price_for_multiple_items_when_1_price_not_found()
        {
            var fakeDisplay = new FakeDisplay();
            var code1 = BarCode.Create("123");
            var code2 = BarCode.Create("456");
            var code3 = BarCode.Create("789");
            var fakePriceService = new FakePriceService()
            {
                { code1, Price.Create(1) },
                { code2, Price.Create(3) }
            };

            var system = Setup.System(fakeDisplay, fakePriceService);
            system.OnBarCode(code1);
            system.OnBarCode(code2);
            system.OnBarCode(code3);
            system.OnTotal();

            Check.That(() => fakeDisplay.LastMessage == $"Total: 4 No price for 789");
        }

        public void show_total_price_for_multiple_items_when_some_prices_not_found()
        {
            var fakeDisplay = new FakeDisplay();
            var code1 = BarCode.Create("123");
            var code2 = BarCode.Create("456");
            var code3 = BarCode.Create("7891");
            var code4 = BarCode.Create("7890");
            var fakePriceService = new FakePriceService()
            {
                { code1, Price.Create(1) },
                { code2, Price.Create(3) }
            };

            var system = Setup.System(fakeDisplay, fakePriceService);
            system.OnBarCode(code1);
            system.OnBarCode(code2);
            system.OnBarCode(code3);
            system.OnBarCode(code4);
            system.OnTotal();

            Check.That(() => fakeDisplay.LastMessage == $"Total: 4 No price for 7891, 7890");
        }

        public void missing_prices_can_be_manually_provided()
        {
            var fakeDisplay = new FakeDisplay();
            var code1 = BarCode.Create("123");
            var code2 = BarCode.Create("456");
            var fakePriceService = new FakePriceService()
            {
                { code1, Price.Create(1) },
            };

            var system = Setup.System(fakeDisplay, fakePriceService);
            system.OnBarCode(code1);
            system.OnBarCode(code2);
            system.OnTotal(Tuple.Create(code2, Price.Create(2)));

            Check.That(() => fakeDisplay.LastMessage == $"Total: 3");
        }
    }
}