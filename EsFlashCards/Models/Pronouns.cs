using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EsFlashCards.Models
{
    public class Pronouns
    {
        [Key]
        public int rowid { get; set; }
        public string Person { get; set; }
        public string Spanish { get; set; }
        public string English { get; set; }
    }
}
