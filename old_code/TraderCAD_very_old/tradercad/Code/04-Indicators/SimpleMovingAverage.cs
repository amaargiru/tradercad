// Read more at TraderCAD.com

using Core;
using System.Linq;

namespace Indicators
{
    public class SimpleMovingAverage : IIndicator
    {
        public IndicatorAnswer Read(IndicatorRequest request)
        {
            int dataLength = request.Data.Length;
            decimal period = request.Coeffs[0] ?? 5;

            IndicatorPoint[] sma = Utility.InitializeArray<IndicatorPoint>(dataLength);

            decimal window = 0;

            for (int i = 0; i < dataLength; ++i)
            {
                window += request.Data[i].Average; // Add the latest entry to the window

                if (i - period + 1 >= 0)
                {
                    sma[i].PointDateTime = request.Data[i].PointDateTime;
                    sma[i].Value = window / period;

                    window -= request.Data[i - (int)period + 1].Average; // Drop off the older entry
                }
            }

            return new IndicatorAnswer
            {
                Indicator = request.Indicator,
                Result = IndicatorResult.Ok,
                Data = sma.ToArray()
            };
        }
    }
}