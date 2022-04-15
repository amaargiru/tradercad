using Core;
using System;
using static System.Math;

namespace ReadOnlyDataProviders
{
    public class RandomDataProviderConnector : IReadOnlyDataProviderConnector
    {
        public ReadDataConnectorAnswer Read(ReadDataConnectorRequest request)
        {
            if (request.StartDateTime > request.EndDateTime)
                throw new ArgumentOutOfRangeException
                    (
                    nameof(request.StartDateTime) + " is bigger than " + nameof(request.EndDateTime) + ".",
                    "DateTime parameters are wrong."
                    );

            int dataLength = (int)Ceiling((request.EndDateTime - request.StartDateTime) / request.Timeframe) + 1;

            FinancePoint[] randomData = new FinancePoint[dataLength];

            for (int i = 0; i < dataLength; i++)
            {
                decimal open = Round(Utility.GetRandomDecimal(0.5m, 1.5m), 5);
                decimal close = Round(Utility.GetRandomDecimal(0.5m, 1.5m), 5);
                decimal high = Round(Max(open, close) + Utility.GetRandomDecimal(0, .1m), 5);
                decimal low = Round(Min(open, close) - Utility.GetRandomDecimal(0, .1m), 5);

                randomData[i] = new FinancePoint
                {
                    PointDateTime = request.StartDateTime.Add(i * request.Timeframe),
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Average = Round((open + close + high + low) / 4m, 5),
                    Volume = Round(Utility.GetRandomDecimal(100_000, 10_000_000), 0)
                };
            }

            return new ReadDataConnectorAnswer
            {
                Equity = request.Equity,
                Result = ReadDataConnectorResult.Ok,
                Data = randomData
            };
        }
    }
}