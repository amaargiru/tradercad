using DataConnectors;
using NUnit.Framework;
using Core;

namespace Tests;
public partial class DataConnectorsTests
{
    private readonly Equities equity = Equities.AAPL;
    private readonly Timeframe timeframe = Timeframe.D1;
    private readonly int pointsLength = points.Length;

    [SetUp]
    public void Cleanup()
    {
        var jsonFileName = Utility.GetJsonFileName(equity, timeframe);

        if (File.Exists(jsonFileName))
        {
            File.Delete(jsonFileName);
        }
    }

    [Test]
    public void JsonFilesDataConnector_DeleteFromNonExistentFile()
    {
        var jsonConn = new JsonFilesDataConnector();
        Assert.IsFalse(jsonConn.Delete(equity, timeframe, singlePoint.PointDateTime));
    }

    [Test]
    public void JsonFilesDataConnector_ReadFromNonExistentFile()
    {
        var jsonConn = new JsonFilesDataConnector();
        Assert.IsNull(jsonConn.Read(equity, timeframe, singlePoint.PointDateTime));
    }

    [Test]
    public void JsonFilesDataConnector_BulkReadFromNonExistentFile()
    {
        var jsonConn = new JsonFilesDataConnector();
        Assert.IsNull(jsonConn.BulkRead(equity, timeframe, points[0].PointDateTime, points[pointsLength - 1].PointDateTime));
    }

    [Test]
    public void JsonFilesDataConnector_CreateNewFileWithSinglePointAndReadIt()
    {
        var jsonConn = new JsonFilesDataConnector();
        Assert.IsTrue(jsonConn.Insert(equity, timeframe, singlePoint, password: "", timeout: 0));
        var point = jsonConn.Read(equity, timeframe, singlePoint.PointDateTime, password: "", timeout: 0);
        Assert.IsTrue(point == singlePoint);
    }

    [Test]
    public void JsonFilesDataConnector_CreateNewFileWithMultiplePointsAndReadThem()
    {
        var jsonConn = new JsonFilesDataConnector();
        var insertNum = jsonConn.BulkInsert(equity, timeframe, points, password: "", timeout: 0);
        Assert.AreEqual(insertNum, points.Length);

        var readPoints = jsonConn.BulkRead(equity, timeframe, points[0].PointDateTime, points[pointsLength - 1].PointDateTime);
        Assert.IsTrue(points.SequenceEqual(readPoints));
    }

    [Test]
    public void JsonFilesDataConnector_CreateNewFileWithMultiplePointsAndAddSinglePointAndReadThemAll()
    {
        var jsonConn = new JsonFilesDataConnector();
        var insertNum = jsonConn.BulkInsert(equity, timeframe, points, password: "", timeout: 0);
        Assert.AreEqual(insertNum, points.Length);

        Assert.IsTrue(jsonConn.Insert(equity, timeframe, singlePoint, password: "", timeout: 0));

        var manualInserted = points.Append(singlePoint).OrderBy(p => p.PointDateTime).ToArray();

        var readPoints = jsonConn.BulkRead(equity, timeframe, points[0].PointDateTime, points[pointsLength - 1].PointDateTime);
        Assert.IsTrue(manualInserted.SequenceEqual(readPoints));
    }

    [Test]
    public void JsonFilesDataConnector_CreateNewFileWithMultiplePointsAndDeleteSinglePointFromItsFile()
    {
        var jsonConn = new JsonFilesDataConnector();
        var manualInserted = points.Append(singlePoint).OrderBy(p => p.PointDateTime).ToArray();
        jsonConn.BulkInsert(equity, timeframe, manualInserted, password: "", timeout: 0);

        Assert.IsTrue(jsonConn.Delete(equity, timeframe, singlePoint.PointDateTime, password: "", timeout: 0));

        var readPoints = jsonConn.BulkRead(equity, timeframe, points[0].PointDateTime, points[pointsLength - 1].PointDateTime);
        Assert.IsTrue(readPoints.SequenceEqual(points));
    }

    private static readonly EquityPoint[] points = new EquityPoint[]
    {
        new EquityPoint
        {
            PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
            Open = 2,
            High = 4,
            Low = 1,
            Close = 3
        },

        // --- Here is one day data gap ---

         new EquityPoint
        {
            PointDateTime = new DateTime(year: 2000, month: 1, day: 3),
            Open = 4,
            High = 6,
            Low = 3,
            Close = 5
        },
         new EquityPoint
        {
            PointDateTime = new DateTime(year: 2000, month: 1, day: 4),
            Open = 5,
            High = 7,
            Low = 4,
            Close = 6
        }
    };

    private static readonly EquityPoint singlePoint = new EquityPoint
    {
        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
        Open = 3,
        High = 5,
        Low = 2,
        Close = 4
    };
}
