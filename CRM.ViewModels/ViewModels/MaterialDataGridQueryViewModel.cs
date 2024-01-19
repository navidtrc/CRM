using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CRM.ViewModels.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MaterialDataGridQueryViewModel
    {
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }

        [JsonProperty(PropertyName = "start")]
        public int start { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int size { get; set; }

        [JsonProperty(PropertyName = "globalFilter")]
        public string globalFilter { get; set; }

        [JsonProperty(PropertyName = "filters")]
        public List<MaterialDataGridQueryFilter> filters { get; set; }

        [JsonProperty(PropertyName = "sorting")]
        public MaterialDataGridQuerySorting sorting { get; set; }

        public DataSourceRequest ToDataSourceRequest()
        {
            return new DataSourceRequest
            {
                Skip = start,
                PageSize = size
            };
        }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MaterialDataGridQueryFilter
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string value { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MaterialDataGridQuerySorting
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public bool desc { get; set; }
    }
}
