// Read more at TraderCAD.com

using Core;
using NUnit.Framework;
using System;
using FullAccessDataProviders;

namespace FullAccessDataProvidersTests
{
    public class JsonTextFilesDataProviderTests
    {
        [Test]
        public void JsonTextFilesDataProvider_Create_BasicFunctionality()
        {
            var createRequest = new CreateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();
            var ans = conn.Create(createRequest);

            Assert.IsTrue(ans.Result == CreateDataConnectorResult.Ok);
        }

        [Test]
        public void JsonTextFilesDataProvider_Create_InvalidPathCharsInEquityNameThrowsException()
        {
            var createRequest = new CreateDataConnectorRequest
            {
                Equity = "TestEquity\0", // Invalid path char '\0' in equity name
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();

            Assert.Throws<ArgumentOutOfRangeException>(() => conn.Create(createRequest));
        }

        [Test]
        public void JsonTextFilesDataProvider_CreateAndRead_BasicFunctionality()
        {
            var createRequest = new CreateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var readRequest = new ReadDataConnectorRequest
            {
                Equity = "TestEquity",
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 1),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();
            conn.Create(createRequest);
            var ans = conn.Read(readRequest);

            Assert.IsTrue(ans.Result == ReadDataConnectorResult.Ok);
            Assert.IsTrue(ans.Data is not null && ans.Data[0].Average == 2.5m);
        }

        [Test]
        public void JsonTextFilesDataProvider_Read_InvalidPathCharsInEquityNameThrowsException()
        {
            var readRequest = new ReadDataConnectorRequest
            {
                Equity = "TestEquity\0", // Invalid path char '\0' in equity name
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 1),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();

            Assert.Throws<ArgumentOutOfRangeException>(() => conn.Read(readRequest));
        }

        [Test]
        public void JsonTextFilesDataProvider_CreateUpdateAndRead_BasicFunctionality()
        {
            var createRequest = new CreateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var updateRequest = new UpdateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
                        Open = 20,
                        High = 40,
                        Low = 10,
                        Close = 30,
                        Average = 25,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var readRequest = new ReadDataConnectorRequest
            {
                Equity = "TestEquity",
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 2),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();
            conn.Create(createRequest);
            conn.Update(updateRequest);
            var ans = conn.Read(readRequest);

            Assert.IsTrue(ans.Result == ReadDataConnectorResult.Ok);
            Assert.IsTrue(ans.Data is not null && ans.Data[0].Average == 2.5m);
            Assert.IsTrue(ans.Data is not null && ans.Data[1].Average == 25);
        }

        [Test]
        public void JsonTextFilesDataProvider_CreateUpdateDeleteAndRead_BasicFunctionality()
        {
            var createRequest = new CreateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 1),
                        Open = 2,
                        High = 4,
                        Low = 1,
                        Close = 3,
                        Average = 2.5m,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var updateRequest = new UpdateDataConnectorRequest
            {
                Equity = "TestEquity",
                Data = new FinancePoint[]
                {
                    new()
                    {
                        PointDateTime = new DateTime(year: 2000, month: 1, day: 2),
                        Open = 20,
                        High = 40,
                        Low = 10,
                        Close = 30,
                        Average = 25,
                        Volume = 1000
                    }
                },
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var deleteRequest = new DeleteDataConnectorRequest
            {
                Equity = "TestEquity",
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 1),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var readRequest = new ReadDataConnectorRequest
            {
                Equity = "TestEquity",
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 2),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new JsonFilesDataProviderConnector();
            conn.Create(createRequest);
            conn.Update(updateRequest);
            conn.Delete(deleteRequest);
            var ans = conn.Read(readRequest);

            Assert.IsTrue(ans.Result == ReadDataConnectorResult.Ok);
            Assert.IsTrue(ans.Data is not null && ans.Data[0].Average == 25);
        }
    }
}