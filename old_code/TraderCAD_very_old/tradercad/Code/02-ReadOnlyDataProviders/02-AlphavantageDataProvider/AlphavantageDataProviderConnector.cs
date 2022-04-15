using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Configuration;

namespace ReadOnlyDataProviders
{
    public class AlphavantageDataProviderConnector : IReadOnlyDataProviderConnector
    {
        public ReadDataConnectorAnswer Read(ReadDataConnectorRequest request)
        {
            string responseString = Alphavantage.JsonRequest
                (
                function: "TIME_SERIES_DAILY",
                outputsize: "full",
                datatype: "json",
                symbol: request.Equity,
                apiKey: request.Password ?? throw new ArgumentNullException("Alphavantage ApiKey is null")
                );

            AlphavantageStockResponse ticks = JsonConvert.DeserializeObject<AlphavantageStockResponse>(responseString);

            int dataLength = ticks.TimeSeriesDaily.Count;
            FinancePoint[] financeData = new FinancePoint[dataLength];

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            int i = dataLength - 1;

            foreach (var tick in ticks.TimeSeriesDaily)
            {
                decimal open = decimal.Parse(tick.Value.Open);
                decimal close = decimal.Parse(tick.Value.Close);

                financeData[i--] = new FinancePoint
                {
                    PointDateTime = DateTime.Parse(tick.Key),
                    Open = open,
                    Close = close,
                    High = decimal.Parse(tick.Value.High),
                    Low = decimal.Parse(tick.Value.Low),
                    Average = (open + close) / 2,
                    Volume = decimal.Parse(tick.Value.Volume),
                };
            }

            var answer = new ReadDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = ReadDataConnectorResult.Ok,
                Data = financeData
            };

            return answer;
        }
    }
}