using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ExampleDotnetCore.LanguagePractice
{
    public class SomeTestClass
    {
        public int Variable;

        public SomeTestClass() { }

        public SomeTestClass(int variable) => Variable = variable;

    }

    public class BaseTest
    {
        public void CheckForeachBehaviour()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            foreach (var item in list)
            {
                // Compilation error
                //item++;
            }

            for (int index = 0; index < list.Count; index++)
            {
                var item = list[index];
                item++;
            }

            list.ForEach(Console.WriteLine);
            // 1, 2, 3, 4, 5
        }

        public void CheckForeachWithReferenceType()
        {
            var list = new List<SomeTestClass> { new SomeTestClass(1), new SomeTestClass(2), new SomeTestClass(3), new SomeTestClass(4), new SomeTestClass(5) };

            foreach (var item in list)
            {
                // Compilation error
                // item = new SomeTestClass();
            }

            for (int index = 0; index < list.Count; index++)
            {
                var item = list[index];
                item = new SomeTestClass();
            }

            list.ForEach(item => Console.WriteLine(item.Variable));
            // 1, 2, 3, 4, 5
        }

        [Test]
        public void TestMethod()
        {
            CheckForeachBehaviour();
        }

        [Test]
        public void TestMethod2()
        {
            CheckForeachWithReferenceType();
        }
    }
}
