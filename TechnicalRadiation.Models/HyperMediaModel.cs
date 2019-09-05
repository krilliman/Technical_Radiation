using System.Dynamic;
using Newtonsoft.Json;


//to get the links property into our classes we need to inherit this class
namespace TechnicalRadiation.Models
{
    public class HyperMediaModel
    {
        public HyperMediaModel() { Links = new ExpandoObject(); }
        [JsonProperty(PropertyName = "_links")]
        public ExpandoObject Links { get; set; }
    }
}