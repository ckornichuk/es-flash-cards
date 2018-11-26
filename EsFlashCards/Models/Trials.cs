using System.ComponentModel.DataAnnotations;

namespace EsFlashCards
{
    public class Trials
    {
        [Key]
        public int rowid { get; set; }
        public string Infinitive { get; set; }
        public int MoodId { get; set; }
        public int TenseId { get; set; }
        public int Pass { get; set; }
        public int Total { get; set; }
    }
}