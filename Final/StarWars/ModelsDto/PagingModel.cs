using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.ModelsDto
{
    public class PagingModel
    {
        public Links Link { get; set; }
        public ICollection<Object> Objects { get; set; } 
    }
}
