using System;
using System.Collections.Generic;
using System.Text;

namespace EsFlashCards
{
    class Proctor
    {
        public ISet<string> WordsToWorkOn { get; set; } = new HashSet<string>();

        public void HandleFailure(string word)
        {
            Console.Write("Add to list? (Y/N):");
            var addToList = Console.ReadLine();

            if (addToList.ToLower() == "y")
                WordsToWorkOn.Add(word);

            Console.WriteLine(Environment.NewLine);
        }
    }
}
