using Newtonsoft.Json;
using System.ComponentModel;

namespace UniversityDemo.Models.Paging
{
    public class PagingRequest
    {
        [DefaultValue(1)]
        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [DefaultValue(10)]
        [JsonProperty(PropertyName = "itemsPerPage")]
        public int ItemsPerPage { get; set; }
    }
}