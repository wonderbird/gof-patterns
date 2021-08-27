using System.Collections.Generic;

namespace Bridge
{
    public class SyncLogger : Logger
    {
        public SyncLogger(IMessageStore messageStore)
            : base(messageStore)
        {
        }

        public SyncLogger()
            : this(new MemoryStore())
        {
        }

        public override void Log(string message)
        {
            _messageStore.Add(message);
        }

        public override IList<string> GetAllMessages()
        {
            return _messageStore.GetAllMessages();
        }

        public override void Flush()
        {
        }
    }
}