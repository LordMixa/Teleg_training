using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Teleg_training.DBEntities;
using Teleg_training.Models;

namespace Teleg_training
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<DBProgramList, ModelList>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.CodeName, opt => opt.MapFrom(src => $"{src.Author.Name}{src.Author.ProgramLists.ToList().IndexOf(src)}"))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Difficult, opt => opt.MapFrom(src => src.Difficult))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode))
                .ForMember(dest => dest.Program, opt => opt.MapFrom(src => src.Program));

            CreateMap<DBProduct, ProductModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Proteins, opt => opt.MapFrom(src => src.Proteins))
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Calories))
                .ForMember(dest => dest.Fats, opt => opt.MapFrom(src => src.Fats))
                .ForMember(dest => dest.Carbohydrates, opt => opt.MapFrom(src => src.Carbohydrates));

        }
    }
}
