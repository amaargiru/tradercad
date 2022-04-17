using DataConnectors;

using NUnit.Framework;

namespace Tests;

public partial class DataConnectorsTests
{
    [Test]
    public void RandomDataConnector_Read_BasicFunctionality()
    {
        var conn = new RandomDataConnector();
        var p = conn.Read(equity, timeframe, DateTime.Now);

        Assert.IsTrue(p.High > p.Open && p.High > p.Close && p.Low < p.Open && p.Low < p.Close);
        Assert.IsTrue(p.PointDateTime.Date == DateTime.Now.Date);
    }

    [Test]
    public void RandomDataConnector_BulkRead_BasicFunctionality()
    {
        var conn = new RandomDataConnector();

        var startDateTime = new DateTime(year: 2000, month: 1, day: 1);
        var endDateTime = new DateTime(year: 2000, month: 1, day: 10);
        var p = conn.BulkRead(equity, timeframe, startDateTime, endDateTime);

        Assert.IsTrue(p?.Length == 10);
    }

    [Test]
    public void RandomDataConnector_BulkRead_WrongStartDateTimeAndEndDateTimeThrowsException()
    {
        var conn = new RandomDataConnector();

        // startDateTime is greater than endDateTime
        var startDateTime = new DateTime(year: 2222, month: 2, day: 22);
        var endDateTime = new DateTime(year: 1000, month: 1, day: 1);

        Assert.Throws<ArgumentOutOfRangeException>(() => conn.BulkRead(equity, timeframe, startDateTime, endDateTime));
    }
}
