using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Models
{
    public class Friend : IBaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empty MainCharacter Field")]
        public int MainCharacterID { get; set; }
        public virtual Character MainCharacter { get; set; }
        [Required(ErrorMessage = "Empty FriendCharacter Field")]
        public int FriendCharacterID { get; set; }
        public virtual Character FriendCharacter { get; set; }

    }
}
