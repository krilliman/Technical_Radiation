using TechnicalRadiation.Repositories;
using AutoMapper;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models;

namespace TechnicalRadiation.Service
{
    public class TRService
    {
        private TRRepository _trRepo;
        public TRService(IMapper mapper)
        {
            _trRepo = new TRRepository(mapper);
        }
        public Envelope<NewsItemDto> GetAllNews(int pageSize, int pageNumer)
        {
            return _trRepo.GetAllNews(pageSize, pageNumer);
        }
        public IEnumerable<NewsItemCategories> GetNewsItemCategories()
        {
            return _trRepo.GetNewsItemsCategories();
        }

        public IEnumerable<NewsItemAuthors> GetNewsAuthors()
        {
            return _trRepo.GetNewsAuthors();
        }
        public NewsItemDetailDto GetNewsById(int id)
        {
            return _trRepo.GetNewsById(id);
        }
        public IEnumerable<NewsItemCategories> GetNewsCategoriesById(int id)
        {
            return _trRepo.GetNewsCategoriesById(id);
        }
        public IEnumerable<NewsItemAuthors> GetNewsAuthorsById(int id)
        {
            return _trRepo.GetNewsAuthorsById(id);
        }
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _trRepo.GetAllCategories();
        }
    }
}