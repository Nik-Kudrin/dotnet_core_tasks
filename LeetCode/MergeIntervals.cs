using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleDotnetCore.LeetCode
{
    /// <summary>
    /// https://leetcode.com/problems/merge-intervals/
    /// 
    /// Brute force approach - O(N2) : Check boundaries in 2 nested loops
    /// Approach 2:
    /// - Sort array by start range number => O(log N)
    /// - Then go over sorted array in loop O(N) and check left neighbour
    /// </summary>
    class MergeIntervals
    {
        public int[][] Merge(int[][] intervals)
        {
            if (intervals == null || intervals.GetLength(0) <= 1)
                return intervals;
            var length = intervals.GetLength(0);

            var sortedRanges = intervals.OrderBy(item => item[0]).ToList();
            // using hash map as result storage
            var mergedIntervals = new Dictionary<int, int[]>(length);
            int? lastMergedIntervalsIndex = null;

            // We have to check only neighbours, because we sorted the ranges
            for (var index = 1; index < length; index++)
            {
                int[] leftItem = null;
                if (lastMergedIntervalsIndex.HasValue && lastMergedIntervalsIndex == index - 1)
                    leftItem = mergedIntervals[lastMergedIntervalsIndex.Value];
                else
                    leftItem = sortedRanges[index - 1];

                var currentItem = sortedRanges[index];

                // Try to merge current item and left item (if possible)
                if (currentItem[0] <= leftItem[1])
                {
                    var rightEdge = leftItem[1];
                    if (rightEdge < currentItem[1])
                        rightEdge = currentItem[1];
                    var merged = new[] { leftItem[0], rightEdge };

                    if (lastMergedIntervalsIndex.HasValue && lastMergedIntervalsIndex == index - 1)
                        mergedIntervals.Remove(lastMergedIntervalsIndex.Value);

                    mergedIntervals.Add(index, merged);
                    lastMergedIntervalsIndex = index;
                }
                else // cannot merge left and current => copy current to result
                {
                    // if it was first iteration - copy left to output too
                    if (index == 1)
                        mergedIntervals.Add(0, leftItem);

                    mergedIntervals.Add(index, currentItem);
                    lastMergedIntervalsIndex = index;
                }
            }

            var result = new int[mergedIntervals.Count][];
            var i = 0;
            foreach (var item in mergedIntervals)
                result[i++] = item.Value;

            return result;
        }

        private void PrintResult(int[][] result)
        {
            foreach (var item in result)
            {
                Console.WriteLine($"[{item[0]},{item[1]}]");
            }
        }

        private bool CompareArrays(int[][] expected, int[][] actual)
        {
            if (expected.Count() != actual.Count())
            {
                Console.WriteLine($"Expected lenght is {expected.Count()}. Actual length is {actual.Count()}");
                return false;
            }

            var sortedExpected = expected.OrderBy(item => item[0]).ToList();
            var sortedActual = actual.OrderBy(item => item[0]).ToList();

            for (var index = 0; index < sortedActual.Count; index++)
            {
                var expectedItem = sortedExpected[index];
                var actualItem = sortedActual[index];
                if (actualItem[0] != expectedItem[0] || actualItem[1] != expectedItem[1])
                    return false;
            }

            return true;
        }

        [Test]
        public void TestMerging()
        {
            var mergedResult = Merge(new int[][] { new[] { 2, 5 }, new[] { 1, 3 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 5 } }, mergedResult));
        }

        [Test]
        public void TestMerging1()
        {
            var mergedResult = Merge(new int[][] { new[] { 4, 7 }, new[] { 2, 5 }, new[] { 1, 6 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 7 } }, mergedResult));
        }

        [Test]
        public void TestMerging2()
        {
            var mergedResult = Merge(new int[][] { new[] { 1, 3 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 3 } }, mergedResult));
        }

        [Test]
        public void TestMerging3()
        {
            var mergedResult = Merge(new int[][] { new[] { 4, 5 }, new[] { 1, 2 }, new[] { 5, 8 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 2 }, new int[] { 4, 8 } }, mergedResult));
        }

        [Test]
        public void TestMerging4()
        {
            var mergedResult = Merge(new int[][] { new[] { 3, 6 }, new[] { 8, 11 }, new[] { 1, 2 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 2 }, new[] { 3, 6 }, new[] { 8, 11 } }, mergedResult));
        }

        [Test]
        public void TestMerging5()
        {
            var mergedResult = Merge(new int[][] { new[] { 1, 3 }, new[] { 2, 6 }, new[] { 8, 10 }, new[] { 15, 18 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 6 }, new[] { 8, 10 }, new[] { 15, 18 } }, mergedResult));
        }

        [Test]
        public void TestMerging6()
        {
            var mergedResult = Merge(new int[][] { new[] { 1, 4 }, new[] { 1, 4 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 4 } }, mergedResult));
        }

        [Test]
        public void TestMerging7()
        {
            var mergedResult = Merge(new int[][] { new[] { 2, 3 }, new[] { 4, 5 }, new[] { 6, 7 }, new[] { 8, 9 }, new[] { 1, 10 } });
            PrintResult(mergedResult);
            Assert.IsTrue(CompareArrays(new int[][] { new[] { 1, 10 } }, mergedResult));
        }
    }
}
