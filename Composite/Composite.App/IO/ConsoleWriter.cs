using System;

namespace Composite.App.IO
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message) => Console.Out.WriteLine(message);
    }
}