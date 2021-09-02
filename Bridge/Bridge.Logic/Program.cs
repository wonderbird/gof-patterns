using System;

namespace Bridge
{
    public class Program
    {
        public static Logger Logger { get; set; } = new SyncLogger();

        public static void Main(string[] args)
        {
            Logger.Log("Hello World!");
            Logger.Log("Hello, Stefan");

            Logger.Flush();
            var messages = Logger.GetAllMessages();
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}