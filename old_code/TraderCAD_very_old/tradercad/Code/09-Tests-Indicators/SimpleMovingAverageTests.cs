// Read more at TraderCAD.com

using Core;
using Indicators;
using NUnit.Framework;
using System;

namespace IndicatorsTests
{
    public class SimpleMovingAverageTests
    {
        [Test]
        public void SimpleMovingAverage_BasicFunctionality_Period2()
        {
            var request = new IndicatorRequest
            {
                Coeffs = new decimal?[] { 2 },
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
                        Open = 20,
                        High = 40,
                        Low = 10,
                        Close = 30,
                        Average = 25,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 3),
                        Open = 200,
                        High = 400,
                        Low = 100,
                        Close = 300,
                        Average = 250,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 4),
                        Open = 2000,
                        High = 4000,
                        Low = 1000,
                        Close = 3000,
                        Average = 2500,
                        Volume = 1000
                    },
                },
            };

            var indicator = new SimpleMovingAverage();
            IndicatorAnswer ans = indicator.Read(request);

            Assert.IsTrue(ans.Result == IndicatorResult.Ok);
            Assert.IsTrue(ans is not null && ans.Data[1]?.Value == 13.75m);
            Assert.IsTrue(ans is not null && ans.Data[2]?.Value == 137.5m);
            Assert.IsTrue(ans is not null && ans.Data[3]?.Value == 1375);
        }

        [Test]
        public void SimpleMovingAverage_BasicFunctionality_Period3()
        {
            var request = new IndicatorRequest
            {
                Coeffs = new decimal?[] { 3 },
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
                        Open = 20,
                        High = 40,
                        Low = 10,
                        Close = 30,
                        Average = 25,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 3),
                        Open = 200,
                        High = 400,
                        Low = 100,
                        Close = 300,
                        Average = 250,
                        Volume = 1000
                    },
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 4),
                        Open = 2000,
                        High = 4000,
                        Low = 1000,
                        Close = 3000,
                        Average = 2500,
                        Volume = 1000
                    },
                },
            };

            var indicator = new SimpleMovingAverage();
            IndicatorAnswer ans = indicator.Read(request);

            Assert.IsTrue(ans.Result == IndicatorResult.Ok);
            Assert.IsTrue(ans is not null && ans.Data[2]?.Value == 92.5m);
            Assert.IsTrue(ans is not null && ans.Data[3]?.Value == 925);
        }
    }
}