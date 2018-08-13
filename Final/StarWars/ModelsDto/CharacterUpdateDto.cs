using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.ModelsDto
{
    public class CharacterUpdateDto
    {
        [Required(ErrorMessage = "Empty Name Field")]
        public string Name { get; set; }
    }
}
