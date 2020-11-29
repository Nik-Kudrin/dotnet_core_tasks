using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ExampleDotnetCore.Codility
{
    class Solution
    {
        public int solution(int inputNumber)
        {
            if (inputNumber < 0)
                throw new ArgumentException("Input number must be non-negative");

            var stringRepresentation = inputNumber.ToString();
            var charList = new List<char>(stringRepresentation.ToCharArray());
            charList.Sort();
            charList.Reverse();
            var strReversed = new string(charList.ToArray());

            Console.WriteLine(strReversed);

            return int.Parse(strReversed);
        }

        [Test]
        public void Test()
        {
            solution(213);
        }
    }
}
