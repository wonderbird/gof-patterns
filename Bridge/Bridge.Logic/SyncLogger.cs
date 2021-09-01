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
            MessageStore.Add(message);
        }

        public override IList<string> GetAllMessages()
        {
            return MessageStore.GetAllMessages();
        }

        public override void Flush()
        {
        }
    }
}