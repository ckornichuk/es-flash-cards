using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EsFlashCards
{
    static class Helper
    {
        public static IEnumerable<string> RequestTenses(VocabContext context)
        {
            var validTenses = context.Tenses.Select(m => m.TenseEnglish).ToList();
            return GetValues(validTenses);
        }

        public static IEnumerable<string> RequestMoods(VocabContext context)
        {
            var validMoods = context.Moods.Select(m => m.MoodEnglish).ToList();
            return GetValues(validMoods);
        }

        private static IEnumerable<string> GetValues(List<string> items)
        {
            Console.WriteLine("Select values: ");
            Console.WriteLine(string.Join(Environment.NewLine, items.Select((item, index) => $"{index}) {item}")));
            var indexes = GetCsvIntResponse();
            if (indexes.Max() > items.Count() || indexes.Min() < 0)
                throw new Exception("Invalid value");

            return indexes.Select(i => items[i]);
        }

        private static IEnumerable<int> GetCsvIntResponse()
        {
            return Console.ReadLine().Split(',').Select(r => Int32.Parse(r));
        }

        public static List<string> getWordsFromFile(string existingFile)
        {
            List<string> existingWords = new List<string>();
            if (existingFile.Length > 0)
            {
                if (!File.Exists(existingFile))
                {
                    Console.WriteLine("Couldn't find the file!");
                }
                else
                {
                    existingWords = File.ReadAllLines(existingFile).ToList();
                }
            }

            return existingWords;
        }

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
