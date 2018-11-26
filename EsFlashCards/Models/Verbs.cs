using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class Verbs
    {
        [Key]
        public int rowid { get; set; }
        public String Infinitive { get; set; }
        public String Mood { get; set; }
        public int MoodId { get; set; }
        public String Tense { get; set; }
        public int TenseId { get; set; }
        public String English { get; set; }
        public String Person { get; set; }
        public String Spanish { get; set; }
    }
}
