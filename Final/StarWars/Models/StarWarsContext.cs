using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class StarWarsContext : DbContext
    {
        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options)
        { }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(chE => chE.Character)
                .WithMany(ch => ch.CharacterEpisodes)
               .HasForeignKey(chE => chE.CharacterID);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(chE => chE.Episode)
                .WithMany(e => e.CharacterEpisodes)
                .HasForeignKey(chE => chE.EpisodeID);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.MainCharacter)
                .WithMany(mC => mC.MainCharacterFriends)
                .HasForeignKey(f => f.MainCharacterID); 

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendCharacter)
                .WithMany(mC => mC.Friends)
                .HasForeignKey(f => f.FriendCharacterID).OnDelete(DeleteBehavior.Restrict);
            



        }
    }
}
