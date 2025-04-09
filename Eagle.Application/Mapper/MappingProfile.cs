using AutoMapper;
using Eagle.Application.DTOs.RequestModels;
using Eagle.Application.DTOs.ResponseModels;
using Eagle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventRequestDto, Event>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => (DateTime?)null));

             
            CreateMap<Event, EventResponseDto>()
            .ForMember(dest => dest.AttendeesList, opt => opt.MapFrom(src => src.Attendees));

            CreateMap<Attendee, AttendeeResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));

        }
    }
}
