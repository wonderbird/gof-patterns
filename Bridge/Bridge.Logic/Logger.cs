using System.Collections.Generic;

namespace Bridge
{
    public abstract class Logger
    {
        protected Logger()
            : this(new MemoryStore())
        {
        }

        protected Logger(IMessageStore messageStore) => MessageStore = messageStore;

        protected IMessageStore MessageStore { get; set; }

        public abstract void Log(string message);

        public abstract IList<string> GetAllMessages();
        public abstract void Flush();
    }
}