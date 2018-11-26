using System;
using System.Collections.Generic;
using System.Text;

namespace EsFlashCards
{
    class Vocab
    {   
        public string PhraseEnglish { get; set; }
        public List<string> PhrasesSpanish { get; set; }
        public string Word { get; set; }
        public Trials Trial { get; set; }
    }
}
