 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Models
{
    public class Friend
    {
        public int MainCharacterID { get; set; }
        public virtual Character MainCharacter { get; set; }
        public int FriendCharacterID { get; set; }
        public virtual Character FriendCharacter { get; set; }
    }
}
