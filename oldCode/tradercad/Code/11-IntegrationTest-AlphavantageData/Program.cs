using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Core;
using FullAccessDataProviders;
using Indicators;
using Microsoft.Extensions.Configuration;
using ReadOnlyDataProviders;
using Visualizers;
using Console = Colorful.Console;

namespace IntegrationTest_AlphavantageData
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("TraderCAD, an open source algorithmic trading engine.");
            Console.WriteLine("Start Alphavantage integration test.");

            // Binding appsettings.json

            const string defaultHardcodeEquity = "AAPL"; // If appsettings.json file is missing or not correct
            const decimal defaultHardcodeSmaDefaultPeriod = 5; //
            const int defaultHardcodePictureWidth = 1000; //
            const int defaultHardcodePictureHeight = 500; //

            IConfigurationRoot? appConfig = null;
            try
            {
                appConfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Missing appsettings.json, will use hard-coded variables.", Color.Orange);
            }

            var appSettings = appConfig?.Get<Settings>();
            string equity = appSettings?.DefaultEquity ?? defaultHardcodeEquity;
            decimal? defaultSmaPeriod = appSettings?.DefaultSmaPeriod ?? defaultHardcodeSmaDefaultPeriod;
            int? defaultPictureWidth = appSettings?.DefaultPictureWidth ?? defaultHardcodePictureWidth;
            int? defaultPictureHeight = appSettings?.DefaultPictureHeight ?? defaultHardcodePictureHeight;

            // Binding appsettings_private.json (for Alphavantage ApiKey only)

            IConfigurationRoot? appPrivateConfig = null;
            try
            {
                appPrivateConfig = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings_private.json", false, true)
                    .Build();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Missing appsettings_private.json. You MUST create appsettings_private.json with " +
                                  "your private Alphavantage ApiKey. See appsettings_private_example.json for example. Press any key to exit.",
                    Color.Red);
                System.Console.ReadKey();
                Environment.Exit(0);
            }

            var appPrivateSettings = appPrivateConfig?.Get<PrivateSettings>();
            var alphavantageApiKey = appPrivateSettings?.AlphavantageApiKey;
            if (alphavantageApiKey is null)
            {
                Console.WriteLine("Missing AlphavantageApiKey in appsettings_private.json. " +
                                  "You MUST create variable AlphavantageApiKey in your appsettings_private.json. " +
                                  "See appsettings_private_example.json for example. Press any key to exit.",
                    Color.Red);
                System.Console.ReadKey();
                Environment.Exit(0);
            }

            // Prepare data request

            var timeframe = new TimeSpan(1, 0, 0, 0);
            var startWholeDateTime = new DateTime(1900, 1, 1);
            var startUsableDateTime = new DateTime(2020, 9, 20);
            var endDateTime = Utility.Floor(DateTime.UtcNow, timeframe); // Floor is important for cashing

            var alphavantageDataConnectorRequest = new ReadDataConnectorRequest
            {
                Equity = equity,
                StartDateTime = startWholeDateTime,
                EndDateTime = endDateTime,
                Timeframe = timeframe,
                Password = alphavantageApiKey
            };

            var expectedFileNotExistOrObsolete = true; // Kind of cashing
            var expectedJsonFileName = JsonFilesDataProviderConnector.GetJsonFileName(alphavantageDataConnectorRequest);
            if (File.Exists(expectedJsonFileName))
            {
                var dateTimeDiff = DateTime.UtcNow - File.GetCreationTime(expectedJsonFileName);
                if (dateTimeDiff < timeframe)
                    expectedFileNotExistOrObsolete = false;
                else
                    File.Delete(expectedJsonFileName); // File is too old
            }

            if (expectedFileNotExistOrObsolete)
            {
                var alphavantageDataConnector = new AlphavantageDataProviderConnector();
                var alphavantageDataConnectorAnswer = alphavantageDataConnector.Read(alphavantageDataConnectorRequest);

                // Create JSON file

                var createJsonFileRequest = new CreateDataConnectorRequest
                {
                    Equity = equity,
                    Data = alphavantageDataConnectorAnswer.Data ??
                           throw new InvalidOperationException(
                               nameof(alphavantageDataConnectorAnswer.Data) + " is null"),
                    Timeframe = timeframe
                };

                var createJsonFileConnector = new JsonFilesDataProviderConnector();
                createJsonFileConnector.Create(createJsonFileRequest);
            }

            // Read from JSON file

            var readRequest = new ReadDataConnectorRequest
            {
                Equity = equity,
                StartDateTime = startUsableDateTime,
                EndDateTime = endDateTime,
                Timeframe = timeframe
            };

            var readJsonFileConnector = new JsonFilesDataProviderConnector();
            var readJsonFileAnswer = readJsonFileConnector.Read(readRequest);

            // Implement SMA Indicator

            var indicatorRequest = new IndicatorRequest
            {
                Data = readJsonFileAnswer.Data ??
                       throw new InvalidOperationException(nameof(readJsonFileAnswer.Data) + " is null"),
                Coeffs = new[] {defaultSmaPeriod}
            };

            var indicator = new SimpleMovingAverage();
            var indicatorAnswer = indicator.Read(indicatorRequest);

            // Implement Bitmap Visualizer

            var visualizerRequest = new VisualizerRequest
            {
                Headline = readJsonFileAnswer.Equity + " + " + indicatorRequest.Coeffs[0] + " Day SMA",
                Data = readJsonFileAnswer.Data,
                IndicatorData = indicatorAnswer.Data,
                PictureSettings = new PictureSettings
                {
                    Width = (int) defaultPictureWidth,
                    Height = (int) defaultPictureHeight
                },
                IndicatorDrawsInSeparateChart = false
            };

            var bitmapVisualizer = new GradientBitmapVisualizer();
            var visualizerAnswer = bitmapVisualizer.Read(visualizerRequest);

            using (var bitmap = visualizerAnswer.Bitmap)
            {
                bitmap?.Save($"{equity}-Alphavantage.png", ImageFormat.Png);
            }

            Console.WriteLine("End Alphavantage Integration Test.");
            System.Console.ReadKey();
        }
    }
}