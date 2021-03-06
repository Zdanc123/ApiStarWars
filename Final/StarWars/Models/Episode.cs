﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class Episode : IBaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empty Name Field")]
        public string Name { get; set; }
        public ICollection<CharacterEpisode> CharacterEpisodes { get; set; }

    }
}
