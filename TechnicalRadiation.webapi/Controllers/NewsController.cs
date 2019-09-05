using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Service;
using TechnicalRadiation.Models.Extensions;
using Newtonsoft.Json;
using System.Dynamic;
using TechnicalRadiation.Models;

namespace TechnicalRadiation.webapi.Controllers
{
    [Route("api")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private TRService _trService;
        //private static HyperMediaExtensions _hmExtension;
        public NewsController(IMapper mapper)
        {
            _trService = new TRService(mapper);
        }
        /*******************************************Helper functions ******************************************************/
        private static IEnumerable<NewsItemDto> NewsItemHelper(IEnumerable<NewsItemDto> AllNews, IEnumerable<NewsItemCategories> NewsCategories, IEnumerable<NewsItemAuthors> NewsAuthors)
        {
            foreach(var item in AllNews)
            {
                dynamic objSelf = new ExpandoObject();
                dynamic objEdit = new ExpandoObject();
                dynamic objDelete = new ExpandoObject();
                objSelf.href = objDelete.href = objEdit.href = "api/"+item.Id;
                List<ExpandoObject> CategoriesList = FilerCategories(NewsCategories, item.Id);
                List<ExpandoObject> AuthorList = FilterAuthors(NewsAuthors, item.Id);
                HyperAddHelper(item.Links, objSelf, objEdit, objDelete, AuthorList, CategoriesList);
            }
            return AllNews;
        }
        private static NewsItemDetailDto NewsItemDetailHelper(NewsItemDetailDto NewsDetail, List<ExpandoObject> NewsCategories, List<ExpandoObject> NewsAuthors, int id)
        {
            dynamic objSelf = new ExpandoObject();
            dynamic objEdit = new ExpandoObject();
            dynamic objDelete = new ExpandoObject();
            objSelf.href = objDelete.href = objEdit.href = "api/" + id;
            HyperAddHelper(NewsDetail.Links, objSelf, objEdit, objDelete, NewsAuthors, NewsCategories);
            return NewsDetail;
        }
        private static List<ExpandoObject> FilterAuthors(IEnumerable<NewsItemAuthors> NewsAuthors, int id)
        {
            List<NewsItemAuthors> NewsAuthorsFiler = NewsAuthors.Where(r => r.NewsItemId == id).ToList();
            List<ExpandoObject> AuthorList = new List<ExpandoObject>();
                NewsAuthorsFiler.ForEach(r =>{
                    dynamic objAuthors = new ExpandoObject();
                    objAuthors.href = "api/authors/" + r.AuthorId;
                    AuthorList.Add(objAuthors);
                });
            return AuthorList;
        }
        private static List<ExpandoObject> FilerCategories(IEnumerable<NewsItemCategories> NewsCategories, int id)
        {
            List<ExpandoObject> CategoriesList = new List<ExpandoObject>();
            List<NewsItemCategories> ItemCategories = NewsCategories.Where(r => r.NewsItemId == id).ToList();
            ItemCategories.ForEach( r => {
                dynamic objCategories = new ExpandoObject();
                objCategories.href = "api/categories/" + r.CategoryId;
                CategoriesList.Add(objCategories);
            });
            return CategoriesList;
        }

        public static void HyperAddHelper(ExpandoObject item, dynamic objSelf, dynamic objEdit, dynamic objDelete, IEnumerable<ExpandoObject> AuthorList, IEnumerable<ExpandoObject> CategoriesList)
        {
            HyperMediaExtensions.AddReference(item, "self", objSelf);
            HyperMediaExtensions.AddReference(item, "edit", objEdit);
            HyperMediaExtensions.AddReference(item, "delete", objDelete);
            HyperMediaExtensions.AddListReference(item, "authors", AuthorList);
            HyperMediaExtensions.AddListReference(item, "categories", CategoriesList);
        }

        /*******************************************Helper functions ******************************************************/


        // GET api/values

        /*eftir að gera þetta að envolop og note query parameterin  */
        /* bæta s.s við að default er pageSize 25 og síðan getur user breytt með queryparameter */
        [HttpGet]
        [Route("")]
        public IActionResult GetAllNews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25)
        {
            Envelope<NewsItemDto> AllNews = _trService.GetAllNews(pageSize, pageNumber);
            IEnumerable<NewsItemCategories> NewsCategories = _trService.GetNewsItemCategories();
            IEnumerable<NewsItemAuthors> NewsAuthors = _trService.GetNewsAuthors();
            return Ok(NewsItemHelper(AllNews.Items, NewsCategories, NewsAuthors));
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetNewsById")]
        public IActionResult GetNewsById(int id)
        {
            NewsItemDetailDto NewsDetail = _trService.GetNewsById(id);
            var TmpList = Enumerable.Repeat(NewsDetail,1);
            IEnumerable<NewsItemCategories> NewsCategories = _trService.GetNewsCategoriesById(id);
            IEnumerable<NewsItemAuthors> NewsAuthors = _trService.GetNewsAuthorsById(id);
            List<ExpandoObject> FilteredNewsCategories = FilerCategories(NewsCategories, id);
            List<ExpandoObject> FilteredNewsAuthors = FilterAuthors(NewsAuthors, id);
            return Ok(NewsItemDetailHelper(NewsDetail, FilteredNewsCategories, FilteredNewsAuthors, id));
        }
    }
}
