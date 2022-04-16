using NUnit.Framework;
using Core;

namespace Tests;
public partial class UtilityTests
{
    [TestCase(0, 1)]
    [TestCase(-1000, 1000)]
    [TestCase(0.01, 0.05)]
    public void GetRandomDecimal_BasicFunctionality(decimal min, decimal max)
    {
        var res = Utility.GetRandomDecimal(min, max);
        Assert.IsTrue(res >= min && res <= max);
    }

    // If min >= max, should throw exception
    [TestCase(1, 0)]
    [TestCase(1000, -1000)]
    [TestCase(0.05, 0.01)]
    [TestCase(0, 0)]
    public void GetRandomDecimal_WrongMinAndMaxThrowsException(decimal min, decimal max)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Utility.GetRandomDecimal(min, max));
    }
}
