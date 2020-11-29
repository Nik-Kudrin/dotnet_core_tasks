using NUnit.Framework;

namespace ExampleDotnetCore.LeetCode
{
    /// <summary>
    /// https://leetcode.com/problems/longest-palindromic-substring/
    /// 
    /// In cycle from i = [1, N-1]
    /// Assume, that i - center of potential palindrome  (in cycle check how far we can get to the left and right from i)
    /// </summary>
    class LongestPalindromSubstringVer2
    {

        public string LongestPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= 1) return input;
            string maxPalindrom = string.Empty;

            for (var centerOfSymmetry = 0; centerOfSymmetry < input.Length; centerOfSymmetry++)
            {
                var currentPalindrome = GetSymmetryLength(input, centerOfSymmetry);
                if (maxPalindrom.Length < currentPalindrome.Length)
                    maxPalindrom = currentPalindrome;
            }

            return maxPalindrom;
        }

        private string GetSymmetryLength(string input, int symmetryIndex)
        {
            int leftIndex = symmetryIndex, rightIndex = symmetryIndex;
            string palindrome = string.Empty;

            palindrome = input[symmetryIndex].ToString();

            // in case, if there more then 1 the same symbol at the center "bnccca"
            while (rightIndex + 1 < input.Length && input[leftIndex] == input[rightIndex + 1])
            {
                rightIndex++;
                palindrome = input.Substring(leftIndex, rightIndex - leftIndex + 1);
            }

            // try to find maxim by length symmetry
            while (leftIndex - 1 >= 0 && rightIndex + 1 < input.Length)
            {
                if (input[--leftIndex] != input[++rightIndex])
                    return palindrome;
                else
                    palindrome = input.Substring(leftIndex, rightIndex - leftIndex + 1);
            }

            return palindrome;
        }

        [Test]
        public void ExampleTest()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("aba", instance.LongestPalindrome("abadd"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest1()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("bab", instance.LongestPalindrome("babad"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest2()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("p", instance.LongestPalindrome("palindrome"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest3()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("long gnol", instance.LongestPalindrome("long gnol"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest4()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("rabbar", instance.LongestPalindrome("bar dmtn rabbar"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest5()
        {
            var str = "abababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababababa";

            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual(str, instance.LongestPalindrome(str), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest6()
        {
            var str = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg";

            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", instance.LongestPalindrome(str), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest7()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("bb", instance.LongestPalindrome("bb"), "The length of palindrome is invalid");
        }

        [Test]
        public void ExampleTest8()
        {
            var instance = new LongestPalindromSubstringVer2();
            Assert.AreEqual("ccc", instance.LongestPalindrome("ccc"), "The length of palindrome is invalid");
        }
    }
}
