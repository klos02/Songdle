using System;
using AutoMapper;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;

namespace Songdle.Application.Mappings;

public class TodaysAlbumGameProfile : Profile
{
    public TodaysAlbumGameProfile()
    {
        CreateMap<TodaysAlbumGame, TodaysAlbumGameDto>().ReverseMap();
    }
}
