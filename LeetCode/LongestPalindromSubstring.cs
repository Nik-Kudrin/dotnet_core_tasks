using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleDotnetCore.LeetCode
{
    /// <summary>
    /// https://leetcode.com/problems/longest-palindromic-substring/
    /// 
    /// Solution - create matrix 
    ///      w l l k 
    ///    w 1 0 0 0
    ///    l 0 1 1 0
    ///    l 0 1 1 0
    ///    k 0 0 0 1
    ///    
    /// Find 'biggest' symmetry (around the central diagonal)
    /// </summary>
    class LongestPalindromSubstring
    {
        public string LongestPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var matrix = InitPalindromeCheckingMatrix(input);
            (int startIndex, int endIndex) = FindLongestPalindrome(matrix, input);
            return input.Substring(startIndex, endIndex - startIndex + 1);
        }

        private bool[][] InitPalindromeCheckingMatrix(string inputStr)
        {
            var palindromMatrix = new bool[inputStr.Length][];

            for (var rowIndex = 0; rowIndex < inputStr.Length; rowIndex++)
            {
                palindromMatrix[rowIndex] = new bool[inputStr.Length];

                var baseSymbol = inputStr[rowIndex];
                for (var columnIndex = 0; columnIndex < inputStr.Length; columnIndex++)
                {
                    if (inputStr[columnIndex] == baseSymbol)
                        palindromMatrix[rowIndex][columnIndex] = true;
                }
            }

            return palindromMatrix;
        }

        private (int startIndex, int endIndex) FindLongestPalindrome(bool[][] palindromeMatrix, string input)
        {
            var length = palindromeMatrix.GetLength(0);
            int columnPalindromeIndex = 0, rowPalindromeIndex = 0, maxDiagonalSize = 0;

            for (var rowIndex = 0; rowIndex < length; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < length; columnIndex++)
                {
                    if (palindromeMatrix[rowIndex][columnIndex] && rowIndex != columnIndex && columnPalindromeIndex < columnIndex)
                    {
                        //   Console.WriteLine(input.Substring(rowIndex, columnIndex - rowIndex + 1));
                        (bool isSymmetrical, int diagonalSize) = CheckSymmetry(palindromeMatrix, rowIndex, columnIndex);
                        Console.WriteLine($"{isSymmetrical}, {diagonalSize} : [{rowIndex}][{columnIndex}]");

                        if (isSymmetrical && diagonalSize > maxDiagonalSize)
                        {
                            maxDiagonalSize = diagonalSize;
                            columnPalindromeIndex = columnIndex;
                            rowPalindromeIndex = rowIndex;
                        }
                    }

                }
            }

            return (rowPalindromeIndex, columnPalindromeIndex);
        }

        private (bool, int) CheckSymmetry(bool[][] palindromeMatrix, int endRowIndex, int endColumnIndex)
        {
            int stepsToDiagonal = 0;
            int maxSteps = 0;
            // increase rowIndex & decrease columnIndex
            while (true)
            {
                if (!palindromeMatrix[endRowIndex++][endColumnIndex--])
                    return (false, maxSteps);

                if (endRowIndex > endColumnIndex)
                    stepsToDiagonal--;
                if (endRowIndex < endColumnIndex)
                    stepsToDiagonal++;
                if (endRowIndex == endColumnIndex)
                {
                    // at least did 1 step in case, if were 1 step from diagonal
                    if (stepsToDiagonal == 0)
                        maxSteps = 1;
                    else
                        maxSteps = stepsToDiagonal;
                }

                if (stepsToDiagonal <= 0)
                    return (true, maxSteps);
            }
        }

        [Test]
        public void ExampleTest()
        {
            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("aba", instance.LongestPalindrome("abadd"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest1()
        {
            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("bab", instance.LongestPalindrome("babad"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest2()
        {
            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("p", instance.LongestPalindrome("palindrome"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest3()
        {
            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("long gnol", instance.LongestPalindrome("long gnol"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest4()
        {
            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("rabbar", instance.LongestPalindrome("bar dmtn rabbar"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest5()
        {
            var str = "abababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababa";

            var instance = new LongestPalindromSubstring();
            Assert.AreEqual(str, instance.LongestPalindrome(str), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest6()
        {
            var str = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg";

            var instance = new LongestPalindromSubstring();
            Assert.AreEqual("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", instance.LongestPalindrome(str), "The length of palindrome is invalid");
        }

    }
}
