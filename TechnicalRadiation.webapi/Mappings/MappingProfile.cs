using System.Net.Security;
using System.Security.Cryptography;
using AutoMapper;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.webapi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsItem, NewsItemDto>();
            CreateMap<NewsItem, NewsItemDetailDto>();
            CreateMap<Category, CategoryDto>();
            //Try to find out how to create link in mapping
            //.ForMember(src => src._links)
            //.ForMember(src => src.AuthorStamp, opt => opt.MapFrom<AuthorStampResolver>());
        }
    }
}

//"self": {"href":"api/"src.Id}