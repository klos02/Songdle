using System;
using AutoMapper;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;

namespace Songdle.Application.Mappings;

public class TodaysGameProfile : Profile
{
    public TodaysGameProfile()
    {
        CreateMap<TodaysGame, TodaysGameDto>();
    }
}
