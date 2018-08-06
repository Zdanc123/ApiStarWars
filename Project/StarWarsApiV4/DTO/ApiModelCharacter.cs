using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Models
{
    public class ApiModelCharacter
    {

        public String Name { get; set; }
        public ICollection <string> Episodes { get; set; }
        public ICollection <string> Friends { get; set; }
    }
}
