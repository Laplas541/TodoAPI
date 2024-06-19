using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Application.Commands;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Mappers
{
    public class UpdateMappingProfile: Profile
    {
        public UpdateMappingProfile()
        {
            CreateMap<CommandUpdate, TodoItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id mapping since it's typically not updated
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => src.IsComplete));
        }
    }
}
