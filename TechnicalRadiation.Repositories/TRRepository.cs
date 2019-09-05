using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Models;

namespace TechnicalRadiation.Repositories
{
    public class TRRepository
    {
        private IMapper _mapper;
        public TRRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Envelope<NewsItemDto> GetAllNews(int pageSize, int pageNumber)
        {
            List<NewsItem> NewsOrderedByPD = DataProvider.NewsItems.OrderBy(r => r.PublishDate).ToList();
            IEnumerable<NewsItemDto> NewsList = _mapper.Map<IEnumerable<NewsItemDto>>(NewsOrderedByPD);
            Envelope<NewsItemDto> NewsEnvolope = new Envelope<NewsItemDto>(pageNumber, pageSize, NewsList);
            return NewsEnvolope;
        }
        public IEnumerable<NewsItemCategories> GetNewsItemsCategories()
        {
            return DataProvider.NewsCategories;
        }
        public IEnumerable<NewsItemAuthors> GetNewsAuthors()
        {
            return DataProvider.NewsAuthors;
        }
        public NewsItemDetailDto GetNewsById(int id)
        {
            var enity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if(enity == null){return null;}
            return _mapper.Map<NewsItemDetailDto>(enity);
        }
        public IEnumerable<NewsItemCategories> GetNewsCategoriesById(int id)
        {
            return DataProvider.NewsCategories.Where(r => r.NewsItemId == id);
        }
        public IEnumerable<NewsItemAuthors> GetNewsAuthorsById(int id)
        {
            return DataProvider.NewsAuthors.Where(r => r.NewsItemId == id);
        }
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(DataProvider.CategoryList);
        }
    }
}