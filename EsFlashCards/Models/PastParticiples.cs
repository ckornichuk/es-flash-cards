using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class PastParticiples
    {
        [Key]
        public int rowid { get; set; }
        public String Infinitive { get; set; }
        public String PastParticiple { get; set; }
        public String PastParticipleEnglish { get; set; }
    }
}
