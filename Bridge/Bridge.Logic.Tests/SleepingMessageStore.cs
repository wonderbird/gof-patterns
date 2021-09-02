using System;
using System.Collections.Generic;
using System.Threading;

namespace Bridge.Logic.Tests
{
    public class SleepingMessageStore : IMessageStore
    {
        private readonly TimeSpan _delay;
        private readonly List<string> _messages = new();

        public SleepingMessageStore(TimeSpan delay) => _delay = delay;

        public void Add(string message)
        {
            Thread.Sleep(_delay);
            _messages.Add(message);
        }

        public IList<string> GetAllMessages() => _messages;
    }
}