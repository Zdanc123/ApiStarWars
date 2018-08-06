using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System;

using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Models
{
    public class SWContext : DbContext
    {
        public SWContext(DbContextOptions<SWContext> options) : base(options)
        { }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterEpisode>()
                .HasKey(chE => new { chE.CharacterID, chE.EpisodeID });

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(chE => chE.Character)
                .WithMany(ch => ch.CharacterEpisodes)
               .HasForeignKey(chE => chE.CharacterID);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(chE => chE.Episode)
                .WithMany(e => e.CharacterEpisodes)
                .HasForeignKey(chE => chE.CharacterID);


            modelBuilder.Entity<Friend>()
               .HasKey(f => new { f.MainCharacterID, f.FriendCharacterID });


            modelBuilder.Entity<Friend>()
                .HasOne(f => f.MainCharacter)
                .WithMany(mC => mC.MainCharacterFriends)
                .HasForeignKey(f => f.MainCharacterID).OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendCharacter)
                .WithMany(mC => mC.Friends)
                .HasForeignKey(f => f.FriendCharacterID);
            //builder.Entity<Friendship>().HasOne(e => e.ActionUser).WithOne().HasForeignKey<Friendship>(e => e.ActionUserId);



        }
    }
    
}
