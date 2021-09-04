using System;
using Composite.Logic.Tests;

namespace Composite.App
{
    public class ConsoleReader : IReader
    {
        public string Read() => Console.ReadLine();
    }
}