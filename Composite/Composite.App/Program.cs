﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Composite.Logic.Tests;

namespace Composite.App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Output.Write("Available shapes:");
            Output.Write("Rectangle: rect x y width height (example: rect 1 2 10 20)");
            Output.Write("Enter shape (enter a blank line to finish):");

            var rectangles = new List<Rectangle>();

            string input;
            while (!string.IsNullOrEmpty(input = Input.Read()))
            {
                rectangles.Add(new Rectangle(input));
            }

            Output.Write(rectangles[0].GetBoundingBox().ToString());
        }

        public static IWriter Output { get; set; } = new ConsoleWriter();
        public static IReader Input { get; set; } = new ConsoleReader();
    }
}