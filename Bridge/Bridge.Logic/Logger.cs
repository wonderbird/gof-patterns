using System.Collections.Generic;

namespace Bridge
{
    public abstract class Logger
    {
        protected IMessageStore MessageStore { get; set; }

        protected Logger()
            : this(new MemoryStore())
        {
        }

        protected Logger(IMessageStore messageStore)
        {
            MessageStore = messageStore;
        }

        public abstract void Log(string message);

        public abstract IList<string> GetAllMessages();
        public abstract void Flush();
    }
}