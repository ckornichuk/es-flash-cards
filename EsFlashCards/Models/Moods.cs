using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class Moods
    {
        [Key]
        public int rowid { get; set; }
        public String Mood { get; set; }
        public String MoodEnglish { get; set; }
    }
}
