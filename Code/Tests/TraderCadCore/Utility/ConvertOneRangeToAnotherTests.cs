using Core;

using NUnit.Framework;

namespace Tests;
public partial class UtilityTests
{
    [TestCase(0.5, 0, 0, 1, 100, 50)]
    [TestCase(0.6, 0, 0, 10, 1000, 60)]
    public void ConvertOneRangeToAnother_BasicFunctionality(decimal value, decimal from1, decimal from2, decimal to1, decimal to2, decimal result)
    {
        var res = Utility.ConvertOneRangeToAnother(value, from1, from2, to1, to2);
        Assert.IsTrue(res == result);
    }
}
