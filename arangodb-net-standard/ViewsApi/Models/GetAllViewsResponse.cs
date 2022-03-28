using System.Collections.Generic;

namespace ArangoDBNetStandard.ViewsApi.Models
{
    public class GetAllViewsResponse : ResponseBase
    {
        public List<ViewSummary> Result { get; set; }
    }
}
