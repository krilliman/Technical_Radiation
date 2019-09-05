using System.Collections.Generic;
using System.Dynamic;



//when a object iherits HyperMediaModel then we are able to use some methods(AddListReference and AddReference) on the link property
//Example useage of the property self in _link property
// "self":{href:"api/1"} -> _links.AddReference("self":{href:"api/1"})
namespace TechnicalRadiation.Models.Extensions
{
    public static class HyperMediaExtensions 
    {
        public static void AddListReference<T>(this ExpandoObject item, string key, IEnumerable<T> list) => ((IDictionary<string, object>)item).Add(key, list);
        public static void AddReference<T>(this ExpandoObject item, string key, T value) => ((IDictionary<string, object>)item).Add(key, value);
    }
}