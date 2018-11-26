using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public partial class Infinitives
    {
        [Key]
        public int rowid { get; set; }
        public String Infinitive { get; set; }
        public String InfinitiveEnglish { get; set; }

    }
}
