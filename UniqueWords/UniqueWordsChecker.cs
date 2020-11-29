using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExampleDotnetCore.UniqueWords
{
    public class UniqueWordsChecker
    {
        private readonly string _baseFilePath = @"C:\Users\neon\Downloads\IELTS\Words.txt";
        private readonly string _additionalFilePath = @"C:\Users\neon\Downloads\IELTS\AdditionalWords.txt";
        private readonly string _baseDirFilePath = @"C:\Users\neon\Downloads\IELTS\";
        private readonly string _wordsToAddFilePath = @"C:\Users\neon\Downloads\IELTS\WordsToAdd.txt";

        public IEnumerable<string> DistinctWordsBetweenCollections(IEnumerable<string> baseLines, IEnumerable<string> additionalLines)
        {
            var baseWords = CleanWords(baseLines);
            var additionalWords = CleanWords(additionalLines);

            var wordsToAdd = additionalWords.Except(baseWords).Distinct();
            return wordsToAdd;
        }

        private List<string> CleanWords(IEnumerable<string> sourceList)
        {
            sourceList = sourceList.Distinct();
            var cleanedList = new List<string>(sourceList.Count());

            foreach (var item in sourceList)
            {
                var cleanedWord = item.Trim().ToLowerInvariant();
                if (cleanedWord.Length > 1)
                    cleanedList.Add(cleanedWord);
            }

            return cleanedList;
        }

        [Test]
        public void GenerateDiffFromRealData()
        {
            var baseLines = new List<string>(File.ReadAllLines(_baseFilePath));
            var additionalLines = new List<string>(File.ReadAllLines(_additionalFilePath));

            var wordsToAdd = DistinctWordsBetweenCollections(baseLines, additionalLines);
            File.WriteAllLines(_wordsToAddFilePath, wordsToAdd);

            var iteration = 0;
            var wordsToTake = 400;
            while (iteration * wordsToTake < wordsToAdd.Count())
            {
                var startPosition = iteration * wordsToTake;
                var wordsChunk = wordsToAdd.Skip(startPosition).Take(wordsToTake);

                var englishChunkFileName = Path.Combine(_baseDirFilePath,
                    $"EnglishWords_{startPosition}-{startPosition + wordsToTake}.txt");
                var russianChunkFileName = Path.Combine(_baseDirFilePath,
                    $"RussianWords_{startPosition}-{startPosition + wordsToTake}.txt");
                File.WriteAllLines(englishChunkFileName, wordsChunk);
                var file = File.Create(russianChunkFileName);
                file.Close();

                iteration++;
            }
        }
        [Test]
        public void MatchEnglishAndRussianTranslation()
        {
            var matches = new Dictionary<string, string>()
            {
                { "EnglishWords_0-400.txt", "RussianWords_0-400.txt" },
                { "EnglishWords_400-800.txt", "RussianWords_400-800.txt"},
                { "EnglishWords_800-1200.txt", "RussianWords_800-1200.txt"},
                { "EnglishWords_1200-1600.txt", "RussianWords_1200-1600.txt"},
                { "EnglishWords_1600-2000.txt", "RussianWords_1600-2000.txt"},
                { "EnglishWords_2000-2400.txt", "RussianWords_2000-2400.txt"},
                { "EnglishWords_2400-2800.txt", "RussianWords_2400-2800.txt"},
                { "EnglishWords_2800-3200.txt", "RussianWords_2800-3200.txt"},
                { "EnglishWords_3200-3600.txt", "RussianWords_3200-3600.txt"},
                { "EnglishWords_3600-4000.txt", "RussianWords_3600-4000.txt"},
                { "EnglishWords_4000-4400.txt", "RussianWords_4000-4400.txt"},
                { "EnglishWords_4400-4800.txt", "RussianWords_4400-4800.txt"},
                { "EnglishWords_4800-5200.txt", "RussianWords_4800-5200.txt"},
                { "EnglishWords_5200-5600.txt", "RussianWords_5200-5600.txt"},
                { "EnglishWords_5600-6000.txt", "RussianWords_5600-6000.txt"},
                { "EnglishWords_6000-6400.txt", "RussianWords_6000-6400.txt"},
                { "EnglishWords_6400-6800.txt", "RussianWords_6400-6800.txt"},
                { "EnglishWords_7200-7600.txt", "RussianWords_7200-7600.txt"},
                { "EnglishWords_7600-8000.txt", "RussianWords_7600-8000.txt"},
                { "EnglishWords_8000-8400.txt", "RussianWords_8000-8400.txt"},
                { "EnglishWords_8400-8800.txt", "RussianWords_8400-8800.txt"},
                { "EnglishWords_8800-9200.txt", "RussianWords_8800-9200.txt"},
                { "EnglishWords_9200-9600.txt", "RussianWords_9200-9600.txt"},
                { "EnglishWords_9600-10000.txt", "RussianWords_9600-10000.txt"}
            };

            var translatedWords = new List<string>(11000);

            foreach (var pair in matches)
            {
                var engWords = File.ReadAllLines(Path.Combine(_baseDirFilePath, pair.Key));
                var rusWords = File.ReadAllLines(Path.Combine(_baseDirFilePath, pair.Value));

                for (var index = 0; index < engWords.Count(); index++)
                {
                    translatedWords.Add($"{rusWords[index].ToLowerInvariant()} - {engWords[index]}");
                }
            }

            var filePathWithTranslations = Path.Combine(_baseDirFilePath, "TranslatedNewWords.txt");
            File.WriteAllLines(filePathWithTranslations, translatedWords);
        }

        [Test]
        public void RunTest()
        {
            var baseLines = new[] { "1 ", "2", "3" };
            var additionalLines = new[] { "1", "2", "4", "8" };

            var diff = DistinctWordsBetweenCollections(baseLines, additionalLines);

            foreach (var item in diff)
                Console.WriteLine(item);
        }

        [Test]
        public void RandomizeWords()
        {
            var random = new Random();
            var sourceFilePath = @"C:\Users\neon\Downloads\IELTS\Vocabulary\TranslatedNewWords.txt";
            var randomizedFilePath = @"C:\Users\neon\Downloads\IELTS\Vocabulary\Translated_Randomized.txt";

            var sourceWords = File.ReadAllLines(sourceFilePath);

            var randomizedWords = sourceWords.OrderBy(item => random.NextDouble());

            File.WriteAllLines(randomizedFilePath, randomizedWords);
        }
    }
}
