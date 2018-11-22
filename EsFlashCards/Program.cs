using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EsFlashCards
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Filepath of existing file to read from?");
            var existingFile = Console.ReadLine().Replace("\"","");
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

            using (VocabContext context = new VocabContext())
            {
                var r = new Random();

                IQueryable<Vocab> mapping;

                if (existingWords.Count > 0)
                {
                    mapping = from verb in context.Verbs
                              where verb.Mood == "Indicativo" && verb.Tense == "Presente"
                              join infinitive in existingWords on verb.Infinitive equals infinitive
                              join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                              select new Vocab
                              {
                                  English = pronoun.English + " " + verb.English,
                                  Spanish = pronoun.Spanish + " " + verb.Spanish,
                                  Infinitive = verb.Infinitive
                              };
                }
                else
                {
                    mapping = from verb in context.Verbs
                              where verb.Mood == "Indicativo" && verb.Tense == "Presente"
                              join topVerbs in context.TopVerbs on verb.Infinitive equals topVerbs.Infinitive
                              join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                              select new Vocab
                              {
                                  English = pronoun.English + " " + verb.English,
                                  Spanish = pronoun.Spanish + " " + verb.Spanish,
                                  Infinitive = verb.Infinitive
                              };
                }

                var randomMapping = mapping.OrderBy(product => r.Next());

                HashSet<string> toWorkOn = new HashSet<string>();

                int count = 0;
                var swatch = Stopwatch.StartNew();
                foreach (var m in randomMapping)
                {
                    Console.WriteLine(m.English);
                    var guess = Console.ReadLine();

                    Console.WriteLine(m.Spanish);
                    if (guess.ToLower() != m.Spanish.RemoveDiacritics())
                    {
                        Console.Write("Incorrect.\nAdd to list? (Y/N):");
                        var addToList = Console.ReadLine();

                        if (addToList.ToLower() == "y")
                            toWorkOn.Add(m.Infinitive);
                    }
                    else
                    {
                        Console.WriteLine("Correct!");
                    }

                    if (count % 10 == 0)
                    {
                        Console.Write("Want to stop? (Y/N):");
                        var isStop = Console.ReadLine();
                        if (isStop.ToLower() == "y")
                        {
                            break;
                        }
                    }
                    Console.WriteLine("\n");
                    count++;
                }

                Console.WriteLine("Complete!");
                Console.WriteLine($"Average response time: {swatch.Elapsed.Divide(count).ToString(@"ss\.ff")}");
                Console.WriteLine("You need to work on: " + string.Join(',', toWorkOn));
                File.WriteAllLines($@"C:\Users\Chris\source\spanish-words\{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", toWorkOn);
                Console.ReadLine();
            }
        }

        static string RemoveDiacritics(this string text)
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

    class Vocab
    {
        public string English { get; set; }
        public string Spanish { get; set; }
        public string Infinitive { get; set; }
    }
}
