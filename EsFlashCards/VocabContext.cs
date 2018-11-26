using EsFlashCards.Models;
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
        public DbSet<Trials> Trials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Doing this because db in debug folder is empty?
            optionsBuilder.UseSqlite($@"Data Source=..\..\..\vocab.db");
        }
    }
}