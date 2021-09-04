using System;
using Composite.Logic.Tests;

namespace Composite.App
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string message) => Console.Out.WriteLine(message);
    }
}