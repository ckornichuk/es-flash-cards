using System;
using System.Collections.Generic;
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
            Console.WriteLine(Vocab.PhraseEnglish);
            return Console.ReadLine().ToLower();
        }

        private bool IsCorrect(string guess)
        {
            return guess == Vocab.PhraseSpanish.RemoveDiacritics();
        }

        private void ShowAnswer(bool isCorrect)
        {
            Console.WriteLine("Answer: " + Vocab.PhraseSpanish);
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
