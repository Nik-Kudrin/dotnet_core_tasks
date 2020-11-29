using NUnit.Framework;
using System.Collections.Generic;

namespace ExampleDotnetCore.LeetCode
{
    public class TwoSumSolution
    {
        /*
         * https://leetcode.com/problems/two-sum/
         * 
         * Input: Array of int's. Not use the same item twice. Return indices of 2 items, that item1 + item2 = target
         * public int[] TwoSum(int[] nums, int target)
         */

        public int[] TwoSum(int[] numbers, int target)
        {
            for (var firstIndex = 0; firstIndex < numbers.Length; firstIndex++)
            {
                var numberToFind = target - numbers[firstIndex];

                for (var secondIndex = 0; secondIndex < firstIndex; secondIndex++)
                    if (numbers[secondIndex] == numberToFind)
                        return new[] { firstIndex, secondIndex };
            }

            return null;
        }


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}