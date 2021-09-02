using System;
using System.Diagnostics;
using Xunit;

namespace Bridge.Logic.Tests
{
    public class SyncLoggerTests
    {
        [Fact]
        public void Log_MessageStoreAddWithDelay_ReturnsAfterDelay()
        {
            var expectedMinimumCallDurationMillis = 10;
            var fakeMessageStore =
                new SleepingMessageStore(TimeSpan.FromMilliseconds(expectedMinimumCallDurationMillis));
            var logger = new SyncLogger(fakeMessageStore);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            logger.Log("anything");
            stopwatch.Stop();

            var actualCallDurationMillis = stopwatch.Elapsed.TotalMilliseconds;
            Assert.True(actualCallDurationMillis > expectedMinimumCallDurationMillis);

            // Plausibility of the selected minimum duration
            // The minimum achievable call duration must be smaller
            // than the duration selected for the test!
            fakeMessageStore = new SleepingMessageStore(TimeSpan.FromMilliseconds(0.0));
            logger = new SyncLogger(fakeMessageStore);
            stopwatch.Reset();
            stopwatch.Start();
            logger.Log("anything");
            stopwatch.Stop();

            actualCallDurationMillis = stopwatch.Elapsed.TotalMilliseconds;
            Assert.True(actualCallDurationMillis < expectedMinimumCallDurationMillis);
        }
    }
}