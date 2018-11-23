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
            Config config = new Config();
            Random r = new Random();
            using (VocabContext context = new VocabContext())
            {
                config.Moods = Helper.RequestMoods(context);
                config.Tenses = Helper.RequestTenses(context);
                config.Words = GetWords(context);

                IQueryable<Vocab> mapping = 
                    from verb in context.Verbs
                    join mood in context.Moods on verb.Mood equals mood.Mood
                    join tense in context.Tenses on verb.Tense equals tense.Tense
                    join infinitive in config.Words on verb.Infinitive equals infinitive
                    join enMood in config.Moods on mood.MoodEnglish equals enMood
                    join enTense in config.Tenses on tense.TenseEnglish equals enTense
                    join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                    select new Vocab
                    {
                        PhraseEnglish = pronoun.English + " " + verb.English,
                        PhraseSpanish = pronoun.Spanish + " " + verb.Spanish,
                        Word = verb.Infinitive
                    };

                var randomizedVocab = mapping.ToList().OrderBy(product => r.Next());

                Terminator terminator = new Terminator();
                Proctor proctor = new Proctor();
                foreach (var vocab in randomizedVocab)
                {
                    Tester tester = new Tester(vocab);
                    bool isCorrect = tester.TestAndReturnResult();

                    if (!isCorrect)
                        proctor.HandleFailure(vocab.Word);

                    if (terminator.DoTheyWantToStop())
                        break;
                }

                Console.WriteLine("Complete!");
                Console.WriteLine("You need to work on: " + string.Join(',', proctor.WordsToWorkOn));
                File.WriteAllLines($@"C:\Users\Chris\source\spanish-words\{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", proctor.WordsToWorkOn);
                Console.ReadLine();
            }
        }

        private static List<string> GetWords(VocabContext context)
        {
            Console.Write("Choose from file? (Y/N): ");
            var choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                Console.WriteLine("Filepath of existing file to read from?");
                var existingFile = Console.ReadLine().Replace("\"", "");
                return Helper.getWordsFromFile(existingFile);
            }

            return context.TopVerbs
                .Select(v => v.Infinitive)
                .ToList();
        }

    }
}
