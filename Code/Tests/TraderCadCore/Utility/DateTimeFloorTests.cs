using Core;

using NUnit.Framework;

namespace Tests;
public partial class UtilityTests
{
    [Test]
    public void DateTimeFloor_BasicFunctionalityDays()
    {
        var f = Utility.DateTimeFloor(DateTime.Now, new TimeSpan(days: 1, 0, 0, 0));

        Assert.IsTrue(f.Millisecond == 0 &&
                      f.Second == 0 &&
                      f.Minute == 0 &&
                      f.Hour == 0);
    }

    [Test]
    public void DateTimeFloor_BasicFunctionalityHours()
    {
        var f = Utility.DateTimeFloor(DateTime.Now, new TimeSpan(hours: 1, 0, 0));

        Assert.IsTrue(f.Millisecond == 0 &&
                      f.Second == 0 &&
                      f.Minute == 0);
    }
}
