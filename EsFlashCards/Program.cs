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
            Random r = new Random();
            using (VocabContext context = new VocabContext())
            {
                Config config = new Config()
                {
                    Moods = Helper.RequestMoods(context),
                    Tenses = Helper.RequestTenses(context),
                };
                var trials = Helper.GetWords(context, config.Tenses, config.Moods);

                Console.Clear();

                var allWords =
                    from trial in trials
                    join verb in context.Verbs on new
                    {
                        trial.Infinitive,
                        trial.MoodId,
                        trial.TenseId
                    } equals new
                    {
                        verb.Infinitive,
                        verb.MoodId,
                        verb.TenseId
                    }
                    join pronoun in context.Pronouns on verb.Person equals pronoun.Person
                    select new Vocab
                    {
                        PhraseEnglish = pronoun.English + " " + verb.English,
                        PhrasesSpanish = verb.Spanish.Split('/').Select(v => pronoun.Spanish + " " + v).ToList(),
                        Word = verb.Infinitive,
                        Trial = trial
                    };

                var randomizedVocabs = allWords.ToList().OrderBy(product => r.Next());
                
                Proctor proctor = new Proctor();
                int correctCount = 0;
                foreach (var vocab in randomizedVocabs)
                {
                    Tester tester = new Tester(vocab);
                    bool isCorrect = tester.TestAndReturnResult();

                    var result = context.Trials.SingleOrDefault(t => t.rowid == vocab.Trial.rowid);
                    if (result != null)
                    {
                        if (isCorrect)
                        {
                            result.Pass = result.Pass + 1;
                            correctCount++;
                        }

                        result.Total = result.Total + 1;
                        context.SaveChanges();
                    }

                    //if (!isCorrect)
                    //    proctor.HandleFailure(vocab.Word);
                }

                Console.WriteLine("Complete!");
                Console.WriteLine($"{correctCount} / {randomizedVocabs.Count()} correct.");
                Console.WriteLine($"{(double)correctCount / (double)randomizedVocabs.Count()}%");
                //File.WriteAllLines($@"C:\Users\Chris\source\spanish-words\{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", proctor.WordsToWorkOn);
                Console.ReadLine();
            }
        }
    }
}
