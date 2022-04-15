// Read more at TraderCAD.com

using Core;
using NUnit.Framework;
using System;
using ReadOnlyDataProviders;

namespace ReadOnlyDataProvidersTests
{
    public class RandomDataProviderTests
    {
        [Test]
        public void RandomDataProviderConnector_Read_BasicFunctionality()
        {
            var request10days = new ReadDataConnectorRequest
            {
                StartDateTime = new DateTime(year: 2000, month: 1, day: 1),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 10),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new RandomDataProviderConnector();
            var answer10days = conn.Read(request10days);

            Assert.IsTrue(answer10days.Data != null && answer10days.Data.Length == 10);
        }

        [Test]
        public void RandomDataProviderConnector_Read_WrongStartDateTimeAndEndDateTimeThrowsException()
        {
            var request10days = new ReadDataConnectorRequest
            {
                StartDateTime = new DateTime(year: 2000, month: 1, day: 10),
                EndDateTime = new DateTime(year: 2000, month: 1, day: 1),
                Timeframe = new TimeSpan(days: 1, 0, 0, 0)
            };

            var conn = new RandomDataProviderConnector();

            Assert.Throws<ArgumentOutOfRangeException>(() => conn.Read(request10days));
        }
    }
}