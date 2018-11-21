using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                              join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                              select new
                              {
                                  English = pronoun.English + " " + verb.English,
                                  Spanish = pronoun.Spanish + " " + verb.Spanish
                              };

                var randomMapping = mapping.OrderBy(product => r.Next());

                foreach (var m in randomMapping)
                {
                    Console.WriteLine(m.English);
                    Console.ReadKey();
                    Console.WriteLine(m.Spanish);
                    Console.ReadKey();
                }
            }
        }
    }
}
