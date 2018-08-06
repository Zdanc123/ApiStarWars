using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Models
{
    public class Episode
    {
        public int EpisodeID { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }

    }
}
