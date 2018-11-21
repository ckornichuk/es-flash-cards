using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsFlashCards
{
    public class VocabContext : DbContext
    {
        public DbSet<Pronouns> Pronouns { get; set; }
        public DbSet<Gerunds> Gerunds { get; set; }
        public DbSet<Infinitives> Infinitives { get; set; }
        public DbSet<Moods> Moods { get; set; }
        public DbSet<PastParticiples> PastParticiples { get; set; }
        public DbSet<Tenses> Tenses { get; set; }
        public DbSet<Verbs> Verbs { get; set; }
        public DbSet<TopVerbs> TopVerbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Doing this because db in debug folder is empty?
            optionsBuilder.UseSqlite($@"Data Source=..\..\..\vocab.db");
        }
    }
}

public class TopVerbs
{
    [Key]
    public int rowid { get; set; }
    public string Infinitive { get; set; }
}

public class Pronouns
{
    [Key]
    public int rowid { get; set; }
    public string Person { get; set; }
    public string Spanish { get; set; }
    public string English { get; set; }
}

public class Gerunds
{
    [Key]
    public int rowid { get; set; }
    public String Infinitive { get; set; }
    public String Gerund { get; set; }
    public String GerundEnglish { get; set; }
}

public partial class Infinitives
{
    [Key]
    public int rowid { get; set; }
    public String Infinitive { get; set; }
    public String InfinitiveEnglish { get; set; }

}

public partial class Moods
{
    [Key]
    public int rowid { get; set; }
    public String Mood { get; set; }
    public String MoodEnglish { get; set; }
}

public partial class PastParticiples
{
    [Key]
    public int rowid { get; set; }
    public String Infinitive { get; set; }
    public String PastParticiple { get; set; }
    public String PastParticipleEnglish { get; set; }
}

public partial class Tenses
{
    [Key]
    public int rowid { get; set; }
    public String Tense { get; set; }
    public String TenseEnglish { get; set; }
}

public partial class Verbs
{
    [Key]
    public int rowid { get; set; }
    public String Infinitive { get; set; }
    public String Mood { get; set; }
    public String Tense { get; set; }
    public String English { get; set; }
    public String Person { get; set; }
    public String Spanish { get; set; }
}