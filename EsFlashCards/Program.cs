using System;
using System.Diagnostics;
using System.Linq;

namespace EsFlashCards
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (VocabContext context = new VocabContext())
            {
                var r = new Random();

                var mapping = from verb in context.Verbs
                              where verb.Mood == "Indicativo" && verb.Tense == "Presente"
                              join topVerbs in context.TopVerbs on verb.Infinitive equals topVerbs.Infinitive
                              join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                              select new
                              {
                                  English = pronoun.English + " " + verb.English,
                                  Spanish = pronoun.Spanish + " " + verb.Spanish
                              };

                var randomMapping = mapping.OrderBy(product => r.Next());

                foreach (var m in randomMapping)
                {
                    var swatch = Stopwatch.StartNew();

                    Console.WriteLine(m.English);
                    Console.ReadKey();

                    swatch.Stop();

                    Console.WriteLine(m.Spanish);
                    Console.WriteLine($"Took {swatch.Elapsed.ToString(@"ss\.ff")}");
                    Console.ReadKey();

                    Console.WriteLine(Environment.NewLine);
                }
            }
        }
    }
}
