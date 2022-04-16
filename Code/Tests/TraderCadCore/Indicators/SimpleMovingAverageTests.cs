using Core;

using NUnit.Framework;

namespace Tests;

public class IndicatorsTests
{
    [Test]
    public void SimpleMovingAverage_BasicFunctionality_Period2()
    {

        var data = new EquityPoint[] {
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
          Open = 2,
          High = 4,
          Low = 1,
          Close = 3
      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
          Open = 20,
          High = 40,
          Low = 10,
          Close = 30
      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 3),
          Open = 200,
          High = 400,
          Low = 100,
          Close = 300
      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 4),
          Open = 2000,
          High = 4000,
          Low = 1000,
          Close = 3000
      }
    };

        var coeffs = new decimal[] { 2 };

        var indicator = new SimpleMovingAverage();
        var answer = indicator.Read(data, coeffs);

        Assert.IsTrue(answer is not null && answer[1]?.Value == 13.75m);
        Assert.IsTrue(answer is not null && answer[2]?.Value == 137.5m);
        Assert.IsTrue(answer is not null && answer[3]?.Value == 1375);
    }

    [Test]
    public void SimpleMovingAverage_BasicFunctionality_Period3()
    {
        var data = new EquityPoint[] {

      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
          Open = 2,
          High = 4,
          Low = 1,
          Close = 3

      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
          Open = 20,
          High = 40,
          Low = 10,
          Close = 30
      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 3),
          Open = 200,
          High = 400,
          Low = 100,
          Close = 300
      },
      new() {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 4),
          Open = 2000,
          High = 4000,
          Low = 1000,
          Close = 3000
      },
    };

        var coeffs = new decimal[] { 3 };

        var indicator = new SimpleMovingAverage();
        var answer = indicator.Read(data, coeffs);

        Assert.IsTrue(answer is not null && answer[2]?.Value == 92.5m);
        Assert.IsTrue(answer is not null && answer[3]?.Value == 925);
    }
}