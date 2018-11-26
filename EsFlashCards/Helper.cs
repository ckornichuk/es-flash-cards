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
        private static double forceCast = 1.0000001;
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

        public static IEnumerable<Trials> GetWords(VocabContext context, IEnumerable<string> tenses, IEnumerable<string> moods)
        {
            //Console.Write("Choose from file? (Y/N): ");
            //var choice = Console.ReadLine();
            //if (choice.ToLower() == "y")
            //{
            //    Console.WriteLine("Filepath of existing file to read from?");
            //    var existingFile = Console.ReadLine().Replace("\"", "");
            //    return getWordsFromFile(existingFile);
            //}

            Console.Write("How many strong words? ");
            var strongCount = Int32.Parse(Console.ReadLine());
            Console.Write("How many weak words? ");
            var weakCount = Int32.Parse(Console.ReadLine());
            Console.Write("How many new words? ");
            var newCount = Int32.Parse(Console.ReadLine());

            var subset = from trial in context.Trials
                         join mood in context.Moods on trial.MoodId equals mood.rowid
                         join tense in context.Tenses on trial.TenseId equals tense.rowid
                         where tenses.Contains(tense.TenseEnglish) && moods.Contains(mood.MoodEnglish)
                         select trial;

            var strongs = (from t in subset
                           where t.Total > 0
                             && (t.Pass * forceCast) / t.Total >= .7
                           orderby Guid.NewGuid()
                           select t).Take(strongCount);

            var weaks = (from t in subset
                         where t.Total > 0
                             && (t.Pass * forceCast) / t.Total < .7
                         orderby Guid.NewGuid()
                         select t).Take(weakCount);

            
            var news = (from t in subset
                        where t.Total == 0
                        orderby Guid.NewGuid()
                        select t).Take(newCount);

            return strongs.Union(weaks).Union(news);
        }
    }
}
