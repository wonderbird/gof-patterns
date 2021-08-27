using System.Collections.Generic;

namespace Bridge
{
    internal class MemoryStore : IMessageStore
    {
        private readonly IList<string> _messages = new List<string>();

        public void Add(string message)
        {
            _messages.Add(message);
        }

        public IList<string> GetAllMessages()
        {
            return _messages;
        }
    }
}