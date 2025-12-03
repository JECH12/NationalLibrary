using AutoMapper;
using Infrastructure.Entities;
using NacionalLibrary_Application.Models;

namespace NacionalLibrary_Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Editorial, EditorialModel>().ReverseMap();
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();

        }
    }
}
