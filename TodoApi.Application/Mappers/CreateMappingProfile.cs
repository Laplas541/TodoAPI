using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TodoApi.Application.Commands;
using TodoApi.Domain;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Mappers
{
    public class CreateMappingProfile : Profile
    {
        public CreateMappingProfile()
        {
            CreateMap<CommandCreate, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => false));
        }
    }
}
