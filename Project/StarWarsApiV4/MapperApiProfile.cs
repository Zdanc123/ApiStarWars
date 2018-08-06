using AutoMapper;
using Microsoft.CodeAnalysis;
using StackExchange.Redis;
using StarWarsApiV4.Models;
using StarWarsApiV4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4
{
    public class MapperApiProfile : Profile
    {
        public MapperApiProfile()
        {
            CreateMap<Character, ApiModelCharacter>().
               ForMember(dest => dest.Name, src => src.MapFrom(n => n.Name)).
               ForMember(dest => dest.Friends, opts => opts.MapFrom(src => src.MainCharacterFriends.Select(s => s.FriendCharacter.Name))).
               ForMember(dest => dest.Episodes, opts => opts.MapFrom(src => src.CharacterEpisodes.Select(s => s.Episode.Name)));


            CreateMap<Episode, EpisodeDTO>().
                ForMember(dest => dest.Name, src => src.MapFrom(n => n.Name)).
                ForMember(dest => dest.Characters, opts => opts.MapFrom(src => src.CharacterEpisodes.Select(s => s.Character.Name)));
           


        }
    }
}