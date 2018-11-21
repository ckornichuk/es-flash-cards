using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsFlashCards
{
    public class VocabContext : DbContext
    {
        public DbSet<Gerunds> Gerunds { get; set; }
        public DbSet<Infinitives> Infinitives { get; set; }
        public DbSet<Moods> Moods { get; set; }
        public DbSet<PastParticiples> PastParticiples { get; set; }
        public DbSet<Tenses> Tenses { get; set; }
        public DbSet<Verbs> Verbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Doing this because db in debug folder is empty?
            optionsBuilder.UseSqlite($@"Data Source=..\..\..\vocab.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Verbs>().HasKey(t => new { t.Infinitive, t.Mood, t.Tense });
        }
    }
}

public class Gerunds
{
    [Key]
    public String Infinitive { get; set; }


    public String Gerund { get; set; }

    public String GerundEnglish { get; set; }

}

public partial class Infinitives
{
    [Key]
    public String Infinitive { get; set; }

    public String InfinitiveEnglish { get; set; }

}

public partial class Moods
{
    [Key]
    public String Mood { get; set; }

    public String MoodEnglish { get; set; }

}

public partial class PastParticiples
{
    [Key]
    public String Infinitive { get; set; }


    public String PastParticiple { get; set; }

    public String PastParticipleEnglish { get; set; }

}

public partial class Tenses
{
    [Key]
    public String Tense { get; set; }

    public String TenseEnglish { get; set; }

}

public partial class Verbs
{
    public String Infinitive { get; set; }

    public String Mood { get; set; }

    public String Tense { get; set; }

    public String VerbEnglish { get; set; }

    public String FirstSingular { get; set; }

    public String SecondSingular { get; set; }

    public String ThirdSingular { get; set; }

    public String FirstPlural { get; set; }

    public String SecondPlural { get; set; }

    public String ThirdPlural { get; set; }
}