using System.Collections.Generic;

namespace CovadisDashboard.Models
{
    public class WebsiteDetailModel
    {
        public List<WebsiteLogModel> WebsiteLog { get; set; }
        public WebsiteModel Website { get; set; }
    }
}
