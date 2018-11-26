using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EsFlashCards
{
    class Tester
    {
        public Vocab Vocab { get; set; }

        public Tester(Vocab vocab)
        {
            Vocab = vocab;
        }

        public bool TestAndReturnResult()
        {
            var guess = AskQuestion();
            bool isCorrect = IsCorrect(guess);
            ShowAnswer(isCorrect);
            return isCorrect;
        }

        private string AskQuestion()
        {
            Console.Clear();
            Console.WriteLine(Vocab.PhraseEnglish);
            return Console.ReadLine().ToLower();
        }

        private bool IsCorrect(string guess)
        {
            var answers = Vocab.PhrasesSpanish.Select(p => p.RemoveDiacritics());
            return answers.Contains(guess);
        }

        private void ShowAnswer(bool isCorrect)
        {
            Console.WriteLine("Answer: " + string.Join('/', Vocab.PhrasesSpanish));
            if (isCorrect)
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Incorrect.");
            }
        }
    }
}
