using System;
using System.Linq;

namespace EsFlashCards
{
    class Program
    {
        static void Main(string[] args)
        {
            using (VocabContext context = new VocabContext())
            {
                context.Gerunds
                    .Select(v => v.Infinitive)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
        }
    }
}
