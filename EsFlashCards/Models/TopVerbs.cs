using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class TopVerbs
    {
        [Key]
        public int rowid { get; set; }
        public string Infinitive { get; set; }
    }
}
