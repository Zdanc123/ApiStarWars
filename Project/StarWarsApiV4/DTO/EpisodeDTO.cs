using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.DTO
{
    public class EpisodeDTO
    {
        public string Name { get; set; }
        public ICollection <string> Characters { get; set; }
    }
}
