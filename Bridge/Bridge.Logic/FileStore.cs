using System.Collections.Generic;
using System.IO;

namespace Bridge
{
    public class FileStore : IMessageStore
    {
        private readonly string _filePath;

        public FileStore(string filePath)
        {
            _filePath = filePath;
            File.Create(_filePath).Close();
        }

        public void Add(string message)
        {
            using var sw = new StreamWriter(_filePath, true);
            sw.WriteLine(message);
        }

        public IList<string> GetAllMessages()
        {
            var lineArray = File.ReadAllLines(_filePath);
            return new List<string>(lineArray);
        }
    }
}