using System;

namespace Composite.App.IO
{
    public class ConsoleReader : IReader
    {
        public string Read() => Console.ReadLine();
    }
}