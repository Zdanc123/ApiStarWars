using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.ModelsDto
{
    public class EpisodeDto
    {
        public string Name { get; set; }
        public ICollection<string> Characters { get; set; }
    }
}
