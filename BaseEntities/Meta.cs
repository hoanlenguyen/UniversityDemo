using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace UniversityDemo.BaseEntities
{
    public class Meta
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName = "createdAt")]
        [DefaultValue(null)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "createdBy")]
        [DefaultValue(null)]
        public string CreatedBy { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "createdFrom")]
        [DefaultValue(null)]
        public string CreatedFrom { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isDeleted")]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; } = false;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updateAt")]
        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updateBy")]
        [DefaultValue(null)]
        public string UpdatedBy { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "updateFrom")]
        [DefaultValue(null)]
        public string UpdatedFrom { get; set; }
    }
}