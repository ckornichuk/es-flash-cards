using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class Gerunds
    {
        [Key]
        public int rowid { get; set; }
        public String Infinitive { get; set; }
        public String Gerund { get; set; }
        public String GerundEnglish { get; set; }
    }
}
