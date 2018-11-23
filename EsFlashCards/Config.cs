using System;
using System.Collections.Generic;
using System.Text;

namespace EsFlashCards
{
    class Config
    {
        public IEnumerable<string> Moods { get; set; }
        public IEnumerable<string> Tenses { get; set; }
        public IEnumerable<string> Words { get; set; }
    }
}
