using System;
using AutoMapper;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;

namespace Songdle.Application.Mappings;

public class SongProfile : Profile
{
    public SongProfile()
    {
        CreateMap<Song, SongDto>();
    }
}
