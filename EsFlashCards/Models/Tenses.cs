using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class Tenses
    {
        [Key]
        public int rowid { get; set; }
        public String Tense { get; set; }
        public String TenseEnglish { get; set; }
    }
}
