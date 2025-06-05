using AutoMapper;
using kAI_webAPI.Dtos.Transcript;
using kAI_webAPI.Models.Transcript;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
namespace kAI_webAPI.Mappers
{
    public class TranscriptMapper : Profile
    {
        public TranscriptMapper()
        {
            CreateMap<Transcript, TranscriptDTOs>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.ToString()));
        }
    }
}
