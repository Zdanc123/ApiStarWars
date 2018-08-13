using AutoMapper;
using StarWars.Models;
using StarWars.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Mapper
{
    public class MaperProfile :Profile
    {
        public MaperProfile()
        {
            CreateMap<Character, CharacterDto>().
               ForMember(dest => dest.Name, src => src.MapFrom(n => n.Name)).
               ForMember(dest => dest.Friends, opts => opts.MapFrom(src => src.MainCharacterFriends.Select(s => s.FriendCharacter.Name))).
               ForMember(dest => dest.Episodes, opts => opts.MapFrom(src => src.CharacterEpisodes.Select(s => s.Episode.Name)));


            CreateMap<Episode, EpisodeDto>().
                ForMember(dest => dest.Name, src => src.MapFrom(n => n.Name)).
                ForMember(dest => dest.Characters, opts => opts.MapFrom(src => src.CharacterEpisodes.Select(s => s.Character.Name)));



        }
    }
}
