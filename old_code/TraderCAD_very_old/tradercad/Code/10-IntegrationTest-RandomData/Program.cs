// Read more at TraderCAD.com

using Core;
using FullAccessDataProviders;
using Indicators;
using ReadOnlyDataProviders;
using System;
using System.Drawing.Imaging;
using Visualizers;

namespace RandomData

{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello World! I'm TraderCAD, an open source algorithmic trading engine.");
            Console.WriteLine("Start Random data integration test.");

            const string equity = "RandomData";
            var startDateTime = new DateTime(year: 2000, month: 1, day: 1);
            var endDateTime = new DateTime(year: 2000, month: 3, day: 31);
            var timeframe = new TimeSpan(days: 1, 0, 0, 0);

            // Create random data

            var randomDataConnectorRequest = new ReadDataConnectorRequest
            {
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Timeframe = timeframe
            };

            var randomDataConnector = new RandomDataProviderConnector();
            var randomDataConnectorAnswer = randomDataConnector.Read(randomDataConnectorRequest);

            // Create JSON file

            var createJsonFileRequest = new CreateDataConnectorRequest
            {
                Equity = equity,
                Data = randomDataConnectorAnswer.Data ??
                       throw new InvalidOperationException(nameof(randomDataConnectorAnswer.Data) + " is null"),
                Timeframe = timeframe
            };

            var createJsonFileConnector = new JsonFilesDataProviderConnector();
            createJsonFileConnector.Create(createJsonFileRequest);

            // Read from JSON file

            var readRequest = new ReadDataConnectorRequest
            {
                Equity = equity,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Timeframe = timeframe
            };

            var readJsonFileConnector = new JsonFilesDataProviderConnector();
            var readJsonFileAnswer = readJsonFileConnector.Read(readRequest);

            // Implement SMA Indicator

            var indicatorRequest = new IndicatorRequest
            {
                Data = readJsonFileAnswer.Data ?? throw new InvalidOperationException(nameof(readJsonFileAnswer.Data) + " is null"),
                Coeffs = new decimal?[] { 3 }
            };

            var indicator = new SimpleMovingAverage();
            var indicatorAnswer = indicator.Read(indicatorRequest);

            // Implement Bitmap Visualizer

            var visualizerRequest = new VisualizerRequest
            {
                Headline = "Random data + 3 Day SMA",
                Data = randomDataConnectorAnswer.Data,
                IndicatorData = indicatorAnswer.Data,
                PictureSettings = new PictureSettings
                {
                    Width = 1200,
                    Height = 675
                },
                IndicatorDrawsInSeparateChart = false
            };

            var bitmapVisualizer = new GradientBitmapVisualizer();
            var visualizerAnswer = bitmapVisualizer.Read(visualizerRequest);

            using (var bitmap = visualizerAnswer.Bitmap)
            {
                bitmap?.Save("RandomData.png", ImageFormat.Png);
            }

            Console.WriteLine("End Integration Test.");
        }
    }
}