using System.Collections.Generic;

namespace CovadisDashboard.Models
{
    public class HomeCombinedModel
    {
        public List<ApiModel> apiModel { get; set; }
        public List<WebsiteModel> websiteModel { get; set; }
    }
}
