using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleDotnetCore.LanguagePractice
{
    public class Parent
    {
        public Parent()
        {
            Console.WriteLine("Parent");
        }

        static Parent()
        {
            Console.WriteLine("Static Parent");
        }
    }

    public class Child : Parent
    {
        public Child()
        {
            Console.WriteLine("Child");
        }

        static Child()
        {
            Console.WriteLine("Static Child");
        }
    }
}
