using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class CharacterEpisode : IBaseModel
    {
        public int Id { get; set; }
        public int CharacterID { get; set; }
        public int EpisodeID { get; set; }
        public virtual Character Character { get; set; }
        public virtual Episode Episode { get; set; }

    }
}
