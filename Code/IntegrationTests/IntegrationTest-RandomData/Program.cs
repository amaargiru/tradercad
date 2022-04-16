namespace IntegrationTestRandomData;

class Program
{
    private static void Main()
    {
        Console.WriteLine("Start TraderCAD random data integration test.");

        const string equity = "RandomData";
        var startDateTime = new DateTime(year: 2000, month: 1, day: 1);
        var endDateTime = new DateTime(year: 2000, month: 3, day: 31);
        var timeframe = Core.TimePeriod.Resolve(Core.Timeframe.D1);

        // Create random data

        var randomData = DataConnectors.RandomDataConnector.BulkRead();

/*
        var randomDataConnectorRequest = new ReadDataConnectorRequest
        {
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Timeframe = timeframe
        };

        var randomDataConnector = new RandomDataProviderConnector();
        var randomDataConnectorAnswer = randomDataConnector.Read(randomDataConnectorRequest);
*/
    }
}
