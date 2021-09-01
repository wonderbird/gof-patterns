using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bridge
{
    public class AsyncLogger : Logger
    {
        private readonly IList<Task> _tasks = new List<Task>();

        public AsyncLogger(IMessageStore messageStore)
            : base(messageStore)
        {
        }

        public AsyncLogger()
            : this(new MemoryStore())
        {
        }

        public override void Log(string message)
        {
            _tasks.Add(Task.Run(() => MessageStore.Add(message)));
        }

        public override void Flush()
        {
            Task.WaitAll(_tasks.ToArray());
        }

        public override IList<string> GetAllMessages()
        {
            return MessageStore.GetAllMessages();
        }
    }
}