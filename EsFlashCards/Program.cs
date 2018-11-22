using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

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

                int i = 0;
                foreach (var m in randomMapping)
                {
                    var swatch = Stopwatch.StartNew();

                    Console.WriteLine(m.English);
                    Console.ReadLine();

                    swatch.Stop();

                    Console.WriteLine(m.Spanish);
                    Console.WriteLine($"Took {swatch.Elapsed.ToString(@"ss\.ff")}");
                    Console.WriteLine($"Did you get it wrong? (Press Y)");
                    var response = Console.ReadLine();
                    if (response.ToLower() == "y")
                    {
                        toWorkOn.Add(m.Infinitive);
                    }

                    if (i % 5 == 0)
                    {
                        Console.WriteLine("Want to stop? (Press Y)");
                        var isStop = Console.ReadLine();
                        if (isStop.ToLower() == "y")
                        {
                            break;
                        }
                    }
                    Console.WriteLine("\n");
                    i++;
                }

                Console.WriteLine("Complete!");
                Console.WriteLine("You need to work on: " + string.Join(',', toWorkOn));
                File.WriteAllLines($@"C:\Users\Chris\source\spanish-words\{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", toWorkOn);
                Console.ReadLine();
            }
        }
    }

    class Vocab
    {
        public string English { get; set; }
        public string Spanish { get; set; }
        public string Infinitive { get; set; }
    }
}
