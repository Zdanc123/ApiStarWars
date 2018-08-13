using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.ModelsDto
{
    public class CharacterDto
    {
        
        public String Name { get; set; }
        public ICollection<string> Episodes { get; set; }
        public ICollection<string> Friends { get; set; }
    }
}
