using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Models
{
    public class Character
    {
       

        public int CharacterID { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
        public ICollection<Friend> MainCharacterFriends { get; set; }
        public ICollection<Friend> Friends { get; set; }
    }
}
