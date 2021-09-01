using System;
using System.Diagnostics;
using Xunit;

namespace Bridge.Logic.Tests
{
    public class AsyncLoggerTests
    {
        [Fact]
        public void Log_MessageStoreAddWithDelay_ReturnsBeforeDelay()
        {
            var messageStoreDelay = 100;
            var fakeMessageStore =
                new SleepingMessageStore(TimeSpan.FromMilliseconds(messageStoreDelay));
            var logger = new AsyncLogger(fakeMessageStore);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            logger.Log("anything");
            stopwatch.Stop();

            var actualCallDurationMillis = stopwatch.Elapsed.TotalMilliseconds;

            Assert.True(actualCallDurationMillis < messageStoreDelay);

            logger.Flush();
            Assert.Equal(1, fakeMessageStore.GetAllMessages().Count);
        }
    }
}