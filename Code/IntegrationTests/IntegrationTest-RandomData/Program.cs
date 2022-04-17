using Core;
using DataConnectors;

namespace IntegrationTestRandomData;

class Program
{
    private static void Main()
    {
        Console.WriteLine("Start TraderCAD random data integration test.");

        Equities equity = Equities.RANDOM_TEST_DATA;
        Timeframe timeframe = Timeframe.D1;
        var startDateTime = new DateTime(year: 2000, month: 1, day: 1);
        var endDateTime = new DateTime(year: 2000, month: 3, day: 31);

        // Create random data
        RandomDataConnector randomDataConnector = new();
        var randomData = randomDataConnector.BulkRead(equity, timeframe, startDateTime, endDateTime);
    }
}
