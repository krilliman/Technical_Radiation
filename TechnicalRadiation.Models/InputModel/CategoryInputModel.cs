using System.ComponentModel.DataAnnotations;
namespace TechnicalRadiation.Models.InputModel
{
    public class CategoryInputModel
    {
        [Required]
        [MaxLength(60)]
        public string Name {get;set;}
    }
}