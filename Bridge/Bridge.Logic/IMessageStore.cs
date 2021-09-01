using System.Collections.Generic;

namespace Bridge
{
    public interface IMessageStore
    {
        void Add(string message);
        IList<string> GetAllMessages();
    }
}