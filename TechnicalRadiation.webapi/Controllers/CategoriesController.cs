using System.Collections.Generic;
using System.Dynamic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Service;

namespace TechnicalRadiation.webapi.Controllers
{
    public class CategoriesController : ControllerBase
    {
        private TRService _trService;
        //private static HyperMediaExtensions _hmExtension;
        public CategoriesController(IMapper mapper)
        {
            _trService = new TRService(mapper);
        }

        /*********************Helper Functions ***********************************/
        public static void HyperAddHelper(ExpandoObject item, int Id)
        {
            dynamic obj = new ExpandoObject();
            obj.href = "api/categories/" + Id;
            HyperMediaExtensions.AddReference(item, "self", obj);
            HyperMediaExtensions.AddReference(item, "edit", obj);
            HyperMediaExtensions.AddReference(item, "delete", obj);
        }


        [HttpGet]
        [Route("api/categories")]
        public IActionResult GetAllCategories()
        {
            IEnumerable<CategoryDto> CategoriesList = _trService.GetAllCategories();
            foreach (var item in CategoriesList)
            {
                HyperAddHelper(item.Links, item.Id);
            }
            return Ok(CategoriesList);
        }
    }
}