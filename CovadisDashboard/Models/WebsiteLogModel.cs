
using System.Collections.Generic;

namespace CovadisDashboard.Models
{
    public class WebsiteLogModel
    {
        public int id { get; set; }
        public int websiteID { get; set; }
        public string url { get; set; }
        public string error { get; set; }
        public string timeStamp { get; set; }
        public List<ElementLogModel> Elements { get; set; }
    }
}
